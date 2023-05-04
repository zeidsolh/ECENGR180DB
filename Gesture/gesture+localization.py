import cv2
import mediapipe as mp
import numpy as np
import csv
import datetime

#file = open('output_results.txt','w') dont need this anymore

mp_drawing = mp.solutions.drawing_utils
mp_drawing_styles = mp.solutions.drawing_styles
mp_holistic = mp.solutions.holistic
mp_pose = mp.solutions.pose

prevwristx=0
prevwristy=0

#previousGuesss=np.zeros(7)
potentialGuess=['null','right','left','up','down']
directions=['4','3','1','2']
previousGesture=0

onleftShoulder=False
wasOnLeftShoulder=False
belowShoulder=False
wasBelowShoulder=False
wasInBox=False
firstRun=True
protentialGesture=0

def inBoxCheck(x,y,boxlocation,boxdimension):
    diffx=boxcenter[0]-x
    diffy=boxcenter[1]-y
    if abs(diffx)<=boxdimension and abs(diffy)<=boxdimension:
        return True
    else:
        return False
def directionLeftBoxIn(x,y,boxlocation,boxdimension):
    diffx=boxcenter[0]-x
    diffy=boxcenter[1]-y
    #print("y: " +str(diffy))
    #print("x: " +str(diffx))
    if abs(diffx)>boxdimension and abs(diffy)>boxdimension:
        return -1
    if diffx<=0:
        #left
        if abs(diffx)>boxdimension and abs(diffy)<=boxdimension:
            return 1
    if diffx>=0:
        #right
        if abs(diffx)>boxdimension and abs(diffy)<=boxdimension:
            return 0
    if diffy<=0:
        #below
        if abs(diffy)>boxdimension and abs(diffx)<=boxdimension:
            return 3
    if diffy>=0:
        #up
        if abs(diffy)>boxdimension and abs(diffx)<=boxdimension:
            return 2

    return -1  
    

#For Sending 
def fileToArray(f):
    content=[]
    for line in f:
        content.append(line)
    return content

def arrayshift(a,newline):
    a.append(newline)
    return a[1:]

def arraytostring(a):
    string=''
    for line in a:
        string = string + line
    return string

def sendToUnity(filename,data,buffersize):
    #remove time from this section and include to the call, so could be faster?
    #currTime=str(datetime.datetime.now())
    #datapacket='['+currTime+']'+str(data)+'\n'
    datapacket=data
    sent=False
    while not sent:
        try:
            fil = open(filename, "r+")
            file_append = open('file_append.txt', 'a')
        except:
            print("Couldn't Access File, Trying to send again!")
            continue
        lines = fileToArray(fil)
        if len(lines)<buffersize:
            newtext=arraytostring(lines)+datapacket
            fil.seek(0)
            fil.write(newtext)
        else:
            newlines=arrayshift(lines,datapacket)
            newtext=arraytostring(newlines)
            fil.seek(0)
            fil.truncate(0)
            fil.write(newtext)
        file_append.write(datapacket)
        file_append.close()
        fil.close()
        sent=True   

#file for gesture so it clears it
open("gesturefile.txt", "w").close()
open("file_append.txt", "w").close()


# For webcam input:
cap = cv2.VideoCapture(0)
cap.set(3, 640)
cap.set(4, 420)

dataindex=0
with mp_holistic.Holistic(
    min_detection_confidence=0.5,
    min_tracking_confidence=0.5) as holistic:
  while cap.isOpened():
    recognizegesture='null'
    success, image = cap.read()
    if not success:
      print("Ignoring empty camera frame.")
      # If loading a video, use 'break' instead of 'continue'.
      continue


    width  = cap.get(3)  # float `width`
    height = cap.get(4) 
    #print("width:" +str(width))
    #print("h:" +str(height))
    # To improve performance, optionally mark the image as not writeable to
    # pass by reference.
    image.flags.writeable = False
    image = cv2.cvtColor(image, cv2.COLOR_BGR2RGB)
    results = holistic.process(image)

    # Draw landmark annotation on the image.
    image.flags.writeable = True
    image = cv2.cvtColor(image, cv2.COLOR_RGB2BGR)

    # commneted out this code in attempt to make it run faster

    # mp_drawing.draw_landmarks(
    #     image,
    #     results.pose_landmarks,
    #     mp_holistic.POSE_CONNECTIONS,
    #     landmark_drawing_spec=mp_drawing_styles
    #     .get_default_pose_landmarks_style())
    

    try:

        #get right wrist
        wrist = results.pose_landmarks.landmark[16]
        wristx=int(wrist.x*width)
        wristy=int(wrist.y*height)

        #get  right elbow
        elbow = results.pose_landmarks.landmark[14]
        elbowx=int(elbow.x*width)
        elbowy=int(elbow.y*height)
        
        #get right shoulder
        # rightshoulder = results.pose_landmarks.landmark[12]
        # rightshoulderx=int(rightshoulder.x*width)
        # rightshouldery=int(rightshoulder.y*height)
        #cv2.putText(image,str(shoulderx),(shoulderx,shouldery),cv2.FONT_HERSHEY_PLAIN,2,(255,0,0),2)

        #get left shoulder
        # leftshoulder = results.pose_landmarks.landmark[11]
        # leftshoulderx=int(leftshoulder.x*width)
        # leftshouldery=int(leftshoulder.y*height)
        #cv2.putText(image,str(shoulderx),(shoulderx,shouldery),cv2.FONT_HERSHEY_PLAIN,2,(255,0,0),2)

        #boxdimension
        #boxdimension=int((0.9*abs(leftshoulderx-rightshoulderx))/2)
        boxdimension = 75
        #box for to check if wrist is in

        boxcenter=(elbowx, elbowy)
        boxleft=(boxcenter[0]-boxdimension,boxcenter[1]-boxdimension)
        boxright=(boxcenter[0]+boxdimension,boxcenter[1]+boxdimension)

        #get nose
        nose = results.pose_landmarks.landmark[0]
        nosex=int(nose.x*width)
        nosey=int(nose.y*height)

        inBox=inBoxCheck(wristx,wristy,boxcenter,boxdimension)
        #cv2.putText(image,"In Box: " + str(inBox),(10,100),cv2.FONT_HERSHEY_PLAIN,1,(255,0,0),1)
        #cv2.putText(image,"Most Recent Gesture: " + directions[previousGesture],(300,100),cv2.FONT_HERSHEY_PLAIN,1,(255,0,0),1)

        if wasInBox or firstRun:  
            if firstRun:
                firstRun=False
            if not inBox:
                direction = directionLeftBoxIn(wristx,wristy,boxcenter,boxdimension)
                #print(direction)
                wasInBox=False
                if direction ==-1:
                    #cv2.putText(image,"Gesture: Other ",(300,100),cv2.FONT_HERSHEY_PLAIN,1,(255,0,0),1)
                    pass
                else:
                    previousGesture=direction
                    #cv2.putText(image,"Gesture: " + directions[direction],(300,100),cv2.FONT_HERSHEY_PLAIN,1,(255,0,0),1)
                    recognizegesture=directions[direction]
            else:
                wasInBox = True      
        else:
            if inBox:
                wasInBox=True
            else:
                wasInBox=False
        data=recognizegesture+','+str(nosex)+ ','+str(nosey)
        ##changed to buffer to 3 for my version
        ##added time here to make it potentialllt more accurate time
        dataindex+=1
        currTime=str(datetime.datetime.now())
        datapacket='['+currTime+'],'+str(data)+","+str(dataindex)+'\n'
        sendToUnity("gesturefile.txt",datapacket,40)
        
    except:
        print("Landmark not found")
        pass

    
    
    try:
        cv2.rectangle(image,boxleft,boxright,(0,255,0),2)
    except:
        print("Box Dimensions out of Range")
        pass
    flippedimage =cv2.flip(image, 1)
    cv2.putText(flippedimage,"Most Recent Gesture: " + directions[previousGesture],(300,100),cv2.FONT_HERSHEY_PLAIN,1,(255,0,0),1)
    scale_percent = 60 # percent of original size
    widthscaled = int(width * scale_percent / 100)
    heightscaled = int(height * scale_percent / 100)
    dim = (widthscaled, heightscaled)
  
    # resize image
    resized = cv2.resize(flippedimage, dim, interpolation = cv2.INTER_AREA)
    cv2.imshow('MediaPipe Holistic', resized)
    #cv2.resizeWindow("MediaPipe Holistic", int(width/2),int(height/2))

    if cv2.waitKey(5) & 0xFF == ord('q'):
      break
cap.release()