MODULE Tooling
	CONST robtarget PickTopNest:=[[290.96,484.77,453.43],[0.0108598,-0.228779,0.973417,0.00109877],[0,-1,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
	CONST robtarget AboveNestPlace:=[[290.96,484.75,481.73],[0.0108639,-0.228776,0.973418,0.00107409],[0,-1,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
	CONST robtarget Presentation1:=[[47.21,-211.38,570.96],[0.499469,0.491017,0.559234,-0.443498],[-1,0,-1,1],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
	CONST robtarget AboveNestPick:=[[289.60,495.70,468.63],[0.0108421,-0.228744,0.973426,0.00106362],[0,-1,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
	CONST robtarget NestClear1:=[[293.17,484.76,457.57],[0.0108841,-0.228798,0.973412,0.00112214],[0,-1,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
	CONST robtarget Presentation1_1:=[[47.21,-211.36,570.90],[0.134526,0.239037,-0.704816,0.654215],[-1,-2,4,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    
    
    PROC rRunAtiTool()
        SetDO DOAtiSpindleCompSMC6,1;
        WaitTime\InPos,1;
        SetDO DOAtiSpindleStartSMC4,1;
    ENDPROC

    PROC rGripperClose()
        SetDO DOGripperCloseSMC0,0;
    ENDPROC

    PROC rGripperOpen()
        SetDO DOGripperCloseSMC0,1;
    ENDPROC
    
    FUNC bool rGripperIsOpen()
        IF(DOGripperCloseSMC0 = 1) THEN
            RETURN TRUE;
        ENDIF
        RETURN FALSE;
    ENDFUNC

    PROC rStopRunAtiTool()
        SetDO DOAtiSpindleStartSMC4,0;
        WaitTime 1;
        SetDO DOAtiSpindleCompSMC6,0;
    ENDPROC

    !PROC KeyenceTrigger()
    !    SetDO DO1KeyenceTrigger,1;
    !    WaitTime(0.01);
    !    SetDO DO1KeyenceTrigger,0;
    !ENDPROC

    PROC goHome()
        
        VAR pos CurrentPose;
        CurrentPose:=CPos(\Tool:=tool0\WObj:=wobj0);

        IF CurrentPose.y<-200 THEN
            MoveJ Home_Sensors,v500,z10,Gripper_FS;
        ENDIF

        IF CurrentPose.y>120 THEN
            MoveJ Home_Tool,v500,z10,Gripper_FS;
        ENDIF

        IF CurrentPose.x>0 THEN
            MoveJ Home,v500,z10,Gripper_FS;
        ENDIF

    ENDPROC

ENDMODULE