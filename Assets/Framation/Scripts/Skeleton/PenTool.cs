using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Framation;
using OperationNamespace;
using OpenCvSharp;
using System;
using System.IO;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public class PenTool : MonoBehaviour
{
    [Header("Pen Canvas")]
    [SerializeField] private PenCanvas penCanvas;

    [Header("Dots")]
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] Transform dotParent;
    
    [Header("Lines")]
    [SerializeField] GameObject lineprefab;
    [SerializeField] Transform  lineParent;

    public static PenTool penTool;

    public  static Vector3[] textureVertices;
    public  static List<UpdateAll> newVertices;
    public  static List<Skeleton> skeletons;
    private Skeleton basicSkeleton;
    private Skeleton copySkeleton ;
    private Skeleton stableSkeleton ;
    private Dot prevDot ;
    private Dot maxDot  ;
    private Dot dot     ;
    private Color color;
    private float k     ; 
    private float prevX ;
    private float prevY ;
    private float dX ;
    private float dY ;
    private bool  selectDot = false;
    private bool  moveSkeleton2 ;
    private bool  move ;
    public  bool  doCopySkeleton1 ;
    public static bool  doLinking ;
    private int   lineCounter ;
    private int   counter;
    private int   dotId;
    public static int frameNum;
    public static int frameId;
    private float step;

    private void Start()  {
        penCanvas.OnPenCanvasLeftClickEvent += AddDot;
        InitializeVars();
        frameNum = 24 ; 
        frameId  = 0 ; 
        step    = 0 ; 
    }

    private void Update() {
        
        if(selectDot){
            if(Drawing.deleteDotMode){
                RemoveDot(dot);
                selectDot = false;
                Drawing.deleteDotMode = false;
            }
        } 
        if(move){
            if(k > 0){
                StartCoroutine(TakeScreenshot(frameId + ".png"));
            }
            frameId += 1;
            step  = ( 1.0f / ( float ) ( frameNum - 1 ) ) ;

            if ( k >= (1.0f + step) ) {
                move = false;
            }

            k    = k + step ;

            Skeleton skeleton1 = skeletons[0]; 
            Skeleton skeleton2 = skeletons[1]; 
            if (doCopySkeleton1){
                doCopySkeleton1 = false;
                stableSkeleton = new Skeleton();
                stableSkeleton = CloneSkeleton(skeleton1,out maxDot,true);
            }
            
            Dictionary<UpdateAll, List<Line>> pointLines = new Dictionary<UpdateAll, List<Line>>();

            for(int i = 0 ; i< skeleton1.lines.Count ; i++) {
                
                if ( i == 0 ){
                    skeleton1.lines[i].start.transform.position = Vector3.Lerp(stableSkeleton.lines[i].start.transform.position, skeleton2.lines[i].start.transform.position, k);
                    skeleton1.lines[i].end.transform.position   = Vector3.Lerp(stableSkeleton.lines[i].end.transform.position  , skeleton2.lines[i].end.transform.position  , k);                    
                }else{
                    skeleton1.lines[i].end.transform.position   = Vector3.Lerp(stableSkeleton.lines[i].end.transform.position  , skeleton2.lines[i].end.transform.position  , k);
                }

                HashSet<UpdateAll> uniqueUpdateAll = new HashSet<UpdateAll>();
                foreach(Triangle triangle in Drawable.drawable.output[skeleton1.lines[i]]){
                    if(!uniqueUpdateAll.Contains(triangle.a))
                        uniqueUpdateAll.Add(triangle.a);
                    if(!uniqueUpdateAll.Contains(triangle.b))
                        uniqueUpdateAll.Add(triangle.b);
                    if(!uniqueUpdateAll.Contains(triangle.c))
                        uniqueUpdateAll.Add(triangle.c);
                }

                foreach(UpdateAll updateAll in uniqueUpdateAll){
                    if(!pointLines.ContainsKey(updateAll))
                        pointLines[updateAll] = new List<Line>();
                    pointLines[updateAll].Add(skeleton1.lines[i]);
                }
            
            }  
            foreach(KeyValuePair<UpdateAll, List<Line>> kvp in pointLines){
                UpdateAll result = new UpdateAll(99999999,new Vector3(0, 0, 0));

                foreach(Line line in kvp.Value){
                    Vector3 localCenter = new Vector3(
                        (line.start.transform.position.x + line.end.transform.position.x) / 2 ,
                        (line.start.transform.position.y + line.end.transform.position.y) / 2 ,
                        (line.start.transform.position.z + line.end.transform.position.z) / 2  
                    );

                    UpdateAll cur = new UpdateAll(kvp.Key.id,kvp.Key.vector);

                    cur.vector = cur.vector - localCenter ; 

                    cur.vector = Vector3.Scale(cur.vector , line.scale);
                    cur.vector = cur.vector + line.positionChange ;
                    cur.vector = Quaternion.Euler(0f, 0f,  line.rotationChange) * cur.vector ;
                    
                    cur.vector = cur.vector + localCenter ; 

                    result.vector += cur.vector;
                }
                result.vector /= kvp.Value.Count;

                kvp.Key.vector = result.vector;
                for (int i = 0; i < newVertices.Count; i++){
                    if (newVertices[i].id == kvp.Key.id) {
                        textureVertices[kvp.Key.id] = kvp.Key.vector;
                    }
                }
            }
            
        }
        if(moveSkeleton2){
            maxDot.onDragMoveEvent += MoveMaxDot;
        }
        if(Drawing.controlMaxDotMode) {
            moveSkeleton2 = !moveSkeleton2 ;
            if(moveSkeleton2 ==  false){
                maxDot.GetComponent<Image>().color = color;
                maxDot.onDragMoveEvent = null;
            }else {
                maxDot.GetComponent<Image>().color = Color.blue;
                prevX = maxDot.transform.position.x;
                prevY = maxDot.transform.position.y;
            }
            Drawing.controlMaxDotMode = false;
        }
        if(Drawing.finishMode){
            move = !move;           
            skeletons.Add(copySkeleton);
            for(int i = 0 ; i < copySkeleton.lines.Count ; i++){
                copySkeleton.lines[i].lr.enabled = false;
                copySkeleton.lines[i].start.GetComponent<Image>().enabled = false;
                copySkeleton.lines[i].end.GetComponent<Image>().enabled = false;
            }
            Drawing.finishMode = false;
        }
        if(Drawing.drawSkelton2Mode){
            
            copySkeleton = new Skeleton();
            copySkeleton = CloneSkeleton(basicSkeleton,out maxDot,false);

            skeletons.Add(basicSkeleton);

            color = maxDot.GetComponent<Image>().color;
            maxDot.GetComponent<Image>().color = Color.blue;
            prevX = maxDot.transform.position.x;
            prevY = maxDot.transform.position.y;

            counter = 0 ;
            dotId = 0 ;
            lineCounter = 0 ;
            prevDot = null;
            dot = null;
            doLinking = true;
            Drawing.drawSkelton2Mode  = false;
        }
        if(Drawing.vanishMode){
            Drawable.drawable.isDrawing = true;   
            InitializeVars();
        }
        
    }

    private void InitializeVars(){
        skeletons     = new List <Skeleton>();
        maxDot        = new Dot();
        basicSkeleton = new Skeleton();
        counter       = 0 ;
        dotId         = 0 ;
        lineCounter   = 0 ;
        k = 0.0f ;
        dX = 0 ; 
        dY = 0 ; 
        moveSkeleton2 = true;
        doCopySkeleton1 = true;
        doLinking     = false;
        move     = false;
        penTool  = this;
    }


    private Skeleton CloneSkeleton(Skeleton skeleton, out Dot maxDot, bool hideSkeleton){
        Dictionary<int, Dot> dotsDictionary = new Dictionary<int, Dot>();
        Skeleton copySkeleton = new Skeleton();
        maxDot = Instantiate(dotPrefab , new Vector3(0,-99999999999,0), Quaternion.identity, dotParent).GetComponent<Dot>();
        foreach(Line line in skeleton.lines){
            if(!dotsDictionary.ContainsKey(line.start.id)){
                Dot dot = Instantiate(dotPrefab , line.start.transform.position, Quaternion.identity, dotParent).GetComponent<Dot>();
                dot.id = line.start.id;
                dot.onDragEvent = line.start.onDragEvent;
                dot.OnRightClickEvent  = line.start.OnRightClickEvent;
                dot.OnLeftClickEvent   = line.start.OnLeftClickEvent;
                dotsDictionary[dot.id] = dot;
            }
            if(!dotsDictionary.ContainsKey(line.end.id)){
                Dot dot = Instantiate(dotPrefab , line.end.transform.position, Quaternion.identity, dotParent).GetComponent<Dot>();
                dot.id = line.end.id;
                dot.onDragEvent = line.end.onDragEvent;
                dot.OnRightClickEvent  = line.end.OnRightClickEvent;
                dot.OnLeftClickEvent   = line.end.OnLeftClickEvent;
                dotsDictionary[dot.id] = dot;
            }
        }
        foreach (Line line in basicSkeleton.lines){
            if ( ( line.start.transform.position.y >  maxDot.transform.position.y) || ( line.end.transform.position.y > maxDot.transform.position.y ) ) {
                if(line.start.transform.position.y >= line.end.transform.position.y){
                    maxDot = dotsDictionary[line.start.id];
                }else{
                    maxDot = dotsDictionary[line.end.id];
                }
            }
            line.lr.enabled = false;
            line.start.GetComponent<Image>().enabled = false;
            line.end.GetComponent<Image>().enabled = false;

            Line cloneLine = line.Clone(); 
            cloneLine = Instantiate (lineprefab , Vector3.zero , Quaternion.identity , lineParent).GetComponent<Line>(); ; 
            cloneLine.id = line.id  ;  

            cloneLine.start    = dotsDictionary[line.start.id];
            cloneLine.start.id = dotsDictionary[line.start.id].id;
            cloneLine.start.onDragEvent = dotsDictionary[line.start.id].onDragEvent;
            cloneLine.start.OnRightClickEvent = dotsDictionary[line.start.id].OnRightClickEvent;
            cloneLine.start.OnLeftClickEvent  = dotsDictionary[line.start.id].OnLeftClickEvent;

            cloneLine.end    = dotsDictionary[line.end.id];
            cloneLine.end.id = dotsDictionary[line.end.id].id;
            cloneLine.end.onDragEvent =  dotsDictionary[line.end.id].onDragEvent;
            cloneLine.end.OnRightClickEvent =  dotsDictionary[line.end.id].OnRightClickEvent;
            cloneLine.end.OnLeftClickEvent  =  dotsDictionary[line.end.id].OnLeftClickEvent;

            cloneLine.SetStart(cloneLine.start , cloneLine.start.id) ;
            cloneLine.SetEnd(  cloneLine.end   , cloneLine.end.id  ) ;
            if(hideSkeleton){
                cloneLine.lr.enabled = false;
                cloneLine.start.GetComponent<Image>().enabled = false;
                cloneLine.end.GetComponent<Image>().enabled = false;
            }
            
            copySkeleton.lines.Add(cloneLine);     
        }

        return copySkeleton;
    }

    private bool IsContains(Vector3 vector,List<Vector3> vs){
        bool isContains = false; 
        foreach(Vector3 v in vs){
            if(
                (  (vector.x == v.x) ||
                    (( vector.x >= ( v.x - 0.01 ) ) &&
                     ( vector.x <= ( v.x + 0.01 ) ))
                ) && 
                (  (vector.y == v.y) ||
                    ( vector.y >= ( v.y - 0.01 ) ) &&
                    ( vector.y <= ( v.y + 0.01 ) )  
                )
            ){
                isContains = true;
            }
        }
        return isContains;
    }

    private void AddDot() {
        if (!Drawable.drawable.isDrawing){
            if(counter == 0 ) {
                Dot dot = Instantiate(dotPrefab , GetMousePosition(), Quaternion.identity, dotParent).GetComponent<Dot>();
                dot.onDragEvent += MoveDot;
                dot.OnLeftClickEvent += SelectDot;
                dot.OnRightClickEvent += UnSelectDot;
                prevDot = dot;
                counter = counter + 1;
            } 
            else if (selectDot && prevDot != null ) {
                Dot newDot = Instantiate(dotPrefab , GetMousePosition(), Quaternion.identity, dotParent).GetComponent<Dot>();
                newDot.onDragEvent += MoveDot;
                newDot.OnLeftClickEvent += SelectDot;
                newDot.OnRightClickEvent += UnSelectDot;

                Line line =  Instantiate(lineprefab , Vector3.zero , Quaternion.identity , lineParent).GetComponent<Line>(); 
                line.id = lineCounter  ;  
                line.SetStart(prevDot,prevDot.id) ;
                dotId = dotId + 1 ;
                line.SetEnd(newDot , dotId) ;

                basicSkeleton.lines.Add(line);

                prevDot = newDot;
                counter = counter + 1;
                lineCounter = lineCounter + 1;
                selectDot = false;
            }
            else if ( counter > 0 && prevDot != null ) {
                Dot newDot = Instantiate(dotPrefab  , GetMousePosition(), Quaternion.identity, dotParent).GetComponent<Dot>();
                newDot.onDragEvent += MoveDot;
                newDot.OnLeftClickEvent += SelectDot;
                newDot.OnRightClickEvent += UnSelectDot;

                Line line  = Instantiate(lineprefab , Vector3.zero , Quaternion.identity , lineParent).GetComponent<Line>(); 
                line.id = lineCounter   ;
                line.SetStart(prevDot,dotId);
                dotId = dotId + 1 ;
                line.SetEnd(newDot , dotId) ;

                basicSkeleton.lines.Add(line);

                prevDot = newDot;
                counter = counter + 1;
                lineCounter = lineCounter + 1;
            } 
            else {
                print("please select dot before!! ");
            }
        }
    }

    private void MoveDot(Dot dot) {
        Vector3 mousePos = GetMousePosition();
        if (isInside(mousePos)){ 
            dot.transform.position = dot.transform.position; 
        }else{
            dot.transform.position = mousePos; 
        }
    }

    private void MoveMaxDot(Dot dot) {
        Vector3 mousePos = GetMousePosition();
        if (isInside(mousePos)){ 
            dot.transform.position = dot.transform.position; 
        }else{
            dot.transform.position = mousePos; 
        }

        dX    = dot.transform.position.x - prevX;
        dY    = dot.transform.position.y - prevY;
        prevX = dot.transform.position.x; 
        prevY = dot.transform.position.y;

        for(int i = 0 ; i < copySkeleton.lines.Count ; i++){
            if ( i == 0 ){
                copySkeleton.lines[i].start.transform.position = new Vector3(copySkeleton.lines[i].start.transform.position.x+ dX , copySkeleton.lines[i].start.transform.position.y + dY , 0 ); 
                copySkeleton.lines[i].end.transform.position   = new Vector3(copySkeleton.lines[i].end.transform.position.x  + dX , copySkeleton.lines[i].end.transform.position.y   + dY , 0 );  
            }else{
                copySkeleton.lines[i].end.transform.position   = new Vector3(copySkeleton.lines[i].end.transform.position.x  + dX , copySkeleton.lines[i].end.transform.position.y   + dY , 0 );                   
            }
        }
    }

    private bool isInside(Vector3 pos){
        if (
            pos.x <=  0.1f  || 
            pos.x >=  12.9f || 
            pos.y >= -0.1f  || 
            pos.y <= -9.15 
        )
            return true;
        else 
            return false;
    }

    private void SelectDot(Dot selectedDot) {
        prevDot   = selectedDot;
        dot       = selectedDot;
        selectDot = true;
    }

    private void UnSelectDot(Dot selectedDot) {
        dot  = null;
        selectDot = false;
    }

    private void RemoveDot(Dot dotRemove) {
        Line line = CheckIfLeaf(dotRemove); 
        if ( line != null ){    
            print("it is a leaf");
            Destroy(line.gameObject);
            Destroy(dotRemove.gameObject);
            for(int i = 0; i < basicSkeleton.lines.Count; i++) {
                if (basicSkeleton.lines[i].id == line.id ) {
                    prevDot = null;
                    basicSkeleton.lines.RemoveAt(i);
                }
            }
        } else {
            print("it is not a leaf");
        }
    }

    private Line CheckIfLeaf(Dot  dot) {
        int freq = 0 ; 
        Line line = null ;
        foreach(Line line2 in  basicSkeleton.lines) {
            if(line2.start.id == dot.id || line2.end.id == dot.id){
                line  = line2;
                freq++;
            }
        }
        if(freq == 1 ){
            return line;
        }
        else
            return null;
    }

    private Vector3 GetMousePosition() {
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldMousePosition.z = 0;
        return worldMousePosition;
    }

    private System.Collections.IEnumerator TakeScreenshot(string filename){
        yield return new WaitForEndOfFrame();

        Texture2D screenshotTexture = new Texture2D(
            1505,1080,
            TextureFormat.RGB24,
            false
        );


        screenshotTexture.ReadPixels(
            new UnityEngine.Rect(
                0,0,
                1505,1080
            ),
            0,0
        );

        screenshotTexture.Apply();

        Mat outputTexture = new Mat();
        Cv2.Resize(OpenCvSharp.Unity.TextureToMat(screenshotTexture), outputTexture, new Size(Home.width, Home.height));
        System.IO.File.WriteAllBytes(Home.imagesPath+"\\"+ filename, OpenCvSharp.Unity.MatToTexture(outputTexture).EncodeToPNG());
    }
}