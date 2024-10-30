MODULE CCSUMain
    CONST robtarget Home:=[[209.53,5.16,537.39],[0.00328327,0.0146564,-0.99987,0.00586276],[0,-1,-1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget Home_Sensors:=[[2.66,-209.51,507.39],[0.00651711,-0.708175,-0.706004,0.00177175],[-1,-1,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget Home_tool:=[[4.44,209.54,537.36],[0.00162517,-0.701222,0.712912,-0.00653747],[0,-1,-1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];

    CONST robtarget PartShowKeyence42_10:=[[6.27,-625.88,829.44],[0.494244,0.495626,0.505722,-0.504305],[-1,0,-1,1],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];

    CONST robtarget rPartPick_10:=[[18.54,-407.56,694.58],[0.00384471,-0.708054,0.706111,0.00724216],[-1,-1,2,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget rPartPick_20:=[[53.83,-354.38,421.12],[0.499899,0.497617,-0.505452,0.496987],[-1,0,1,1],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget rPartPick_30:=[[21.49,-364.23,297.89],[0.453944,0.556618,-0.376278,0.585258],[-2,-1,1,1],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget rPartPick_40:=[[6.33,-328.65,294.34],[0.455745,0.555424,-0.377382,0.584281],[-2,-1,1,1],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget rPartPick_50:=[[4.95,-324.08,308.23],[0.463269,0.542982,-0.36796,0.595951],[-2,-1,1,1],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget rPartPick_60:=[[-17.82,-370.17,488.87],[0.449435,0.567565,-0.400681,0.561546],[-2,-1,2,1],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];

    CONST robtarget PartDeburrBottomBrush_10:=[[20.59,466.07,437.01],[0.602728,0.125692,-0.750355,-0.240599],[0,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget PartDeburrBottomBrush_20:=[[131.20,449.67,476.97],[0.272617,0.273039,-0.90518,-0.178268],[0,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget PartDeburrBottomBrush_30:=[[-21.43,481.63,402.00],[0.629363,0.053374,-0.75379,-0.181256],[0,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget PartDeburrBottomBrush_40:=[[-25.46,482.17,428.74],[0.629308,0.0533195,-0.753849,-0.181216],[0,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget PartDeburrBottomBrush_50:=[[33.47,484.19,428.74],[0.629353,0.0534079,-0.753787,-0.181294],[0,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget PartDeburrBottomBrush_60:=[[-12.38,413.96,424.25],[0.492486,-0.605556,-0.452148,0.431649],[0,0,1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget PartDeburrBottomBrush_70:=[[-22.50,400.55,409.83],[0.178115,-0.733386,-0.0522476,0.653981],[0,0,2,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget PartDeburrBottomBrush_80:=[[-30.75,401.69,403.02],[0.17569,-0.713364,-0.0583396,0.6759],[0,0,2,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget PartDeburrBottomBrush_90:=[[-32.46,400.96,430.63],[0.175705,-0.7134,-0.0582814,0.675864],[0,0,2,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget PartDeburrBottomBrush_100:=[[-2.75,400.95,432.33],[0.223478,-0.707408,-0.109083,0.661613],[0,0,1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget PartDeburrBottomBrush_110:=[[44.13,418.00,447.95],[0.552822,-0.233141,-0.776315,0.193307],[0,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];

    CONST robtarget PartPickToolTop_10:=[[182.71,481.27,319.22],[2.81422E-05,0.252387,-0.967626,5.44977E-05],[0,-1,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget PartPickToolTop_20:=[[184.19,479.05,291.57],[2.52464E-05,0.275963,-0.961168,5.72893E-05],[0,-1,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget PartPickToolTop_30:=[[186.90,474.91,277.87],[5.31134E-05,0.287484,-0.957786,8.70098E-05],[0,-1,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget PartPickToolTop_40:=[[186.90,474.90,378.79],[7.99451E-05,0.287469,-0.95779,0.000131325],[0,-1,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget PartPickToolTop_50:=[[95.27,363.91,463.37],[0.000545139,-0.492715,0.870185,-0.00319877],[0,-1,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];

    CONST robtarget PartPlaceToolTop_10:=[[107.24,363.98,327.23],[0.000830186,-0.460527,0.887641,-0.00281925],[0,-1,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget PartPlaceToolTop_20:=[[188.59,474.22,286.25],[0.000721765,-0.280126,0.959959,-0.00283513],[0,-1,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget PartPlaceToolTop_30:=[[188.60,474.24,277.87],[0.000713492,-0.281875,0.959447,-0.00283385],[0,-1,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget PartPlaceToolTop_40:=[[188.09,478.25,280.22],[0.0006451,-0.281919,0.959433,-0.00292647],[0,-1,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget PartPlaceToolTop_50:=[[188.08,481.96,293.90],[0.000649877,-0.281907,0.959437,-0.00291178],[0,-1,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];

    CONST robtarget PartPickToolSide_10:=[[256.14,495.20,409.71],[0.636266,-0.225868,0.723003,0.146344],[1,-2,1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget PartPickToolSide_20:=[[212.73,468.00,218.72],[0.636279,-0.225846,0.722991,0.146378],[1,-2,2,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget PartPickToolSide_30:=[[205.28,461.34,217.68],[0.637331,-0.253883,0.717265,0.121997],[1,-2,1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget PartPickToolSide_40:=[[262.33,490.31,364.84],[0.63627,-0.225865,0.722995,0.146372],[0,-2,1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget PartPickToolSide_50:=[[180.42,458.49,569.25],[1.78132E-05,0.978994,0.20389,-8.85849E-06],[0,-1,-2,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];

    CONST robtarget rPartShow_10:=[[-16.06,-343.79,434.41],[0.511275,0.499772,0.487462,-0.501205],[-2,-1,0,1],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget rPartShow_20:=[[-25.33,-376.99,647.3],[0.491571,0.508004,0.501758,-0.498527],[-2,-1,0,1],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget rPartShow_30:=[[-22.55,-379.04,726.64],[0.57712,0.601052,0.394229,-0.387625],[-2,-1,0,1],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];

    CONST robtarget cTotePartPick_10:=[[16.46,-508.56,583.95],[0.0183208,-0.713973,0.699779,0.0147405],[-1,0,2,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget cTotePartPick_20:=[[193.84,-562.84,573.2],[0.0180562,-0.70406,0.709754,0.0149481],[-1,0,2,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget cTotePartPick_30:=[[191.27,-556.92,553.93],[0.0227291,-0.938112,0.345535,0.00595582],[-1,0,1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget cTotePartPick_40:=[[191.27,-556.92,524.77],[0.0227312,-0.938112,0.345535,0.00595572],[-1,0,1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget PartPickToolSide_60:=[[254.28,495.13,234.30],[0.636247,-0.225862,0.723024,0.146329],[1,-2,1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget PartPickToolSide_70:=[[235.72,475.43,217.08],[0.639167,-0.225261,0.720439,0.147279],[1,-2,2,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget PartPickToolSide_80:=[[205.66,462.32,230.94],[0.639866,-0.244624,0.71687,0.129721],[1,-2,1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    
    !TASK PERS tooldata CurrentTool:=[TRUE,[[0,0,228],[1,0,0,0]],[3.8,[-0.3,3.9,65.1],[1,0,0,0],0,0,0.006]]; !Totebox_GRIPPER_FS DATA !MOVED TO user SYSTEM MODULE
    
    
    
    !======================
    !=========MAIN========= 
    !======================
    
    !PROC Main()
        !ForceStopRecording;
        !ForceLoadID;
        !ForceInitalization;
     !   MoveJ Home_Sensors,v100,fine,CurrentTool;
        
                
      !  VisionTest;
      !  goHome;
      !  PartDeburrBottomBrush;
        
       ! WaitTime 2;
       ! MoveJ Home_Sensors,v100,fine,CurrentTool;
        !ForceStopRecording;
       ! Stop\AllMoveTasks;
    !ENDPROC

    !======================
    !=======MAIN END=======
    !======================

    


    !for picking up part from single part stand on uppr platform
    PROC PartPickSensors()
        MoveJ Home_Sensors,v100,fine,CurrentTool;

        !part Home point
        MoveJ rPartPick_10,v500,z50,tool0;
        !move to Part
        MoveJ rPartPick_20,v500,fine,tool0;
        !move to pickup pre-orientation
        MoveL rPartPick_30,v100,fine,tool0;

        rGripperOpen;

        MoveL rPartPick_40,v40,fine,tool0;
        WaitTime 1.5;
        !grabing and correcting the part orientation 
        rGripperClose;
        rGripperOpen;
        rGripperClose;
        WaitTime 1.5;

        MoveL rPartPick_50,v100,z50,tool0;
        MoveL rPartPick_60,v100,z50,tool0;
        MoveJ rPartPick_10,v500,z50,tool0;
        MoveJ Home_Sensors,v100,fine,CurrentTool;
    ENDPROC



    !for showing off grabbed items to the CognexCamera
    PROC PartShow()
        !part Home point
        MoveJ rPartPick_10,v500,z50,tool0;
        !show start position
        MoveJ rPartShow_10,v30,z50,tool0;
        MoveJ rPartShow_20,v30,z50,tool0;
        MoveJ rPartShow_30,v30,z50,tool0;
        !take photo_1



    ENDPROC


    !to pickup and place items from tote slot B8 (lower left corner) and returns to part pick Home
    PROC PartPickTote()
        !move to part pick Home
        MoveJ rPartPick_10,v100,z50,tool0;

        !get into position  
        MoveJ cTotePartPick_10,v100,z50,tool0;
        !move to selection level
        MoveJ cTotePartPick_20,v100,z50,tool0;
        !move above slot B8
        rGripperOpen;

        !grab
        MoveL cTotePartPick_30,v30,z30,tool0;
        !orientate
        MoveL cTotePartPick_40,v10,z10,tool0;
        !move downward to pickup part
        rGripperClose;

        !return to part Home
        MoveL cTotePartPick_20,v100,z10,tool0;
        MoveJ cTotePartPick_10,v100,z50,tool0;
        MoveJ rPartPick_10,v100,z50,tool0;

    ENDPROC



    !picks up the bracket when placed on the tool plate and picking up from the top.  
    PROC PartPickToolTop()

        MoveJ Home_tool,v500,z50,CurrentTool;

        MoveJ PartPickToolTop_10,v150,z50,CurrentTool;
        rGripperOpen;
        MoveL PartPickToolTop_20,v10,fine,CurrentTool;
        MoveL PartPickToolTop_30,v5,fine,CurrentTool;
        rGripperClose;
        WaitTime\InPos,1;
        MoveL PartPickToolTop_40,v150,z0,CurrentTool;

        MoveJ Home_tool,v500,z50,CurrentTool;


    ENDPROC

    
    
    !places a part back on the tool plate when gripping upright
    PROC PartPlaceToolTop()

        MoveJ Home_tool,v500,z50,CurrentTool;

        MoveJ PartPlaceToolTop_10,v150,z50,CurrentTool;
        MoveJ PartPlaceToolTop_20,v80,z50,CurrentTool;
        MoveJ PartPlaceToolTop_30,v5,z50,CurrentTool;
        WaitTime\InPos,1;
        rGripperOpen;
        MoveJ PartPlaceToolTop_40,v30,fine,CurrentTool;
        MoveJ PartPlaceToolTop_50,v30,fine,CurrentTool;

        MoveL PartPickToolTop_40,v150,z0,CurrentTool;


        MoveJ Home_tool,v500,z50,CurrentTool;


    ENDPROC



    !picks up the part on the tool plate from the side.
    PROC PartPickToolSide()

        MoveJ Home_tool,v500,z50,CurrentTool;

        MoveJ PartPickToolSide_10,v150,z50,CurrentTool;
        MoveJ PartPickToolSide_60,v40,z50,CurrentTool;
        MoveL PartPickToolSide_70,v20,z50,CurrentTool;
        rGripperOpen;
        MoveL PartPickToolSide_20,v10,fine,CurrentTool;
        MoveL PartPickToolSide_30,v5,fine,CurrentTool;
        WaitTime\InPos,1;
        rGripperClose;
        MoveL PartPickToolSide_80,v5,fine,CurrentTool;
        MoveL PartPickToolSide_40,v150,z0,CurrentTool;

        MoveJ Home_tool,v500,z50,CurrentTool;

    ENDPROC

    

    !deburrs the bottom edge of the part above the lettering with the brush
    PROC PartDeburrBottomBrush()

        !due to spindle speeds brush waits for all doors to be closed
        WaitDI DILeftDoorClosed,1;
        WaitDI DimiddleDoorClosed,1;
        WaitDI DIRightDoorClosed,1;

        rRunAtiTool;

        ! move close to tool.
        MoveJ Home_tool,v500,z50,CurrentTool;
        MoveJ PartDeburrBottomBrush_20,v150,z50,CurrentTool;
        MoveJ PartDeburrBottomBrush_10,v150,z50,CurrentTool;

        !slowly move from top down to deburr bottom side.
        MoveJ PartDeburrBottomBrush_30,v30,fine,CurrentTool;
        Movel PartDeburrBottomBrush_40,v5,fine,CurrentTool;
        MoveL PartDeburrBottomBrush_30,v5,fine,CurrentTool;

        !flip part deburr angle.
        MoveJ PartDeburrBottomBrush_50,v80,z50,CurrentTool;
        MoveJ PartDeburrBottomBrush_60,v150,z50,CurrentTool;
        MoveJ PartDeburrBottomBrush_70,v150,z50,CurrentTool;

        !Move from top down to deburr opposite side. 
        MoveJ PartDeburrBottomBrush_80,v5,fine,CurrentTool;
        MoveL PartDeburrBottomBrush_90,v5,fine,CurrentTool;
        MoveL PartDeburrBottomBrush_80,v5,fine,CurrentTool;
        MoveJ PartDeburrBottomBrush_100,v50,z50,CurrentTool;


        !move away and back to Tool home
        MoveJ PartDeburrBottomBrush_110,v50,z50,CurrentTool;
        rStopRunAtiTool;
        MoveJ PartDeburrBottomBrush_20,v150,z50,CurrentTool;
        MoveJ Home_tool,v500,z50,CurrentTool;

    ENDPROC

    
    
    ! this program will show edge 42 of previously grabbed vane to the keyence sensor for instpection. IT RETURNS BACK TO HOME_SENSORS WHEN DONE.
    !PROC PartShowKeyence42()
        
    !    MoveJ Home_Sensors, v50, z50, CurrentTool;
    !    MoveJ PartShowKeyence42_10,v500,z50,CurrentTool;
        
    !    WaitRob\ZeroSpeed;
    !    KeyenceTrigger;
    !    WaitDI DI2KeyenceBusy,1;!TILL NOT BUSY FALLING EDGE AND WAIT HALF A SECOND
    !    WaitDI DI2KeyenceBusy,0;
    !    WaitTime(0.5);
    !    
    !    
    !    MoveJ Home_Sensors, v50, z50, CurrentTool;
    !
    !ENDPROC

    
    
ENDMODULE