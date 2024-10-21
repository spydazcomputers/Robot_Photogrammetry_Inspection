MODULE Tooling
    
    
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