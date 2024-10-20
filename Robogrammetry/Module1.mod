MODULE Module1
    PERS robtarget p20:=[[152.478,185.808,207.487 ],[0.367323,0.741513,-0.346174,0.442036],[-1,0,-1,11],[152.879,9E+09,9E+09,9E+09,9E+09,9E+09]];
    !***********************************************************
    !
    ! Module:  Module1
    !
    ! Description:
    !   <Insert description here>
    !
    ! Author: ryanm
    !
    ! Version: 1.0
    !
    !***********************************************************
    
    
    !***********************************************************
    !
    ! Procedure main
    !
    !   This is the entry point of your program
    !
    !***********************************************************
    PROC main()
        MoveJ p20, v5000, fine, tool0;
    ENDPROC
ENDMODULE