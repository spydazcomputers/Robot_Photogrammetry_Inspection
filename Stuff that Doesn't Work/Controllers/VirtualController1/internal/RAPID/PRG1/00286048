MODULE TRob1Main
    !***********************************************************
    ! Program data
    !***********************************************************
    ! Home position.
    LOCAL CONST jointtarget home := [[0, -30, 30, 0, 90, 0], [9E9, 9E9, 9E9, 9E9, 9E9, 9E9]];
    
    ! Identifier for the EGM correction.
    LOCAL VAR egmident egm_id;
    
    ! Limits for convergance.
    LOCAL VAR egm_minmax egm_condition := [-0.01, 0.01];
            
    ! EGM pose frames.
    LOCAL CONST pose egm_correction_frame := [[0, 0, 0], [1, 0, 0, 0]];
    LOCAL CONST pose egm_sensor_frame     := [[0, 0, 0], [1, 0, 0, 0]];
    
    ! The work object (here set to coincidence with ROB_2's base frame).
    LOCAL PERS wobjdata egm_wobj := [FALSE, TRUE, "", [[0, 1000, 0],[1, 0, 0, 0]], [[0, 0, 0],[1, 0, 0, 0]]];
    
    PROC main()
        TPWrite "Entered main()";
        MoveL Home_Sensors, v200, fine, tool0;
        
        ! Register an EGM id.
        EGMGetId egm_id;
            
        ! Setup the EGM communication.
        EGMSetupUC ROB_1, egm_id, "default", "ROB_1", \Pose;
        
        WHILE TRUE DO
            ! Prepare for an EGM communication session.
            EGMActPose egm_id,
                       \WObj:=wobj0,
                       egm_correction_frame,
                       EGM_FRAME_BASE,
                       egm_sensor_frame,
                       EGM_FRAME_BASE
                       \X:=egm_condition
                       \Y:=egm_condition
                       \Z:=egm_condition
                       \Rx:=egm_condition
                       \Ry:=egm_condition
                       \Rz:=egm_condition
                       \MaxSpeedDeviation:=100.0;
                        
            ! Start the EGM communication session.
            ! EGMRunPose egm_id, EGM_STOP_RAMP_DOWN, \X \Y \Z \Rx \Ry \Rz \CondTime:=5 \RampOutTime:=5;
            
            !EGMRunPose egm_id, EGM_STOP_RAMP_DOWN, \X \Y \Z \CondTime:=30 \RampOutTime:=5;
            !TPWrite "Movising to position";
            EGMRunPose egm_id, EGM_STOP_HOLD, \X \Y \Z \Rx \Ry \Rz \CondTime:=0.01 \RampInTime:=0.05 \RampOutTime:=0.05;! \PosCorrGain:=0;
            
            !WaitTime 0.05;
        ENDWHILE
        
        ! Release the EGM id.
            EGMReset egm_id;
    ERROR
        IF ERRNO = ERR_UDPUC_COMM THEN
            TPWrite "Communication timedout";
            TRYNEXT;
        ENDIF
    ENDPROC

ENDMODULE