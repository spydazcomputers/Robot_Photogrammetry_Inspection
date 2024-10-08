MODULE MainModule
	PERS robtarget p20:=[[152.478,185.808,207.487 ],[0.367323,0.741513,-0.346174,0.442036],[-1,0,-1,11],[152.879,9E+09,9E+09,9E+09,9E+09,9E+09]];
	PERS robtarget p30:=[[152.478,185.808,207.487],[0.367323,0.741513,-0.346174,0.442036],[-1,0,-1,11],[152.879,9E+09,9E+09,9E+09,9E+09,9E+09]];
	    VAR num P20_X:=0;
	VAR num P20_Y:=0;
    VAR num P20_Z:=0;
    VAR jointtarget jointpos1;
    VAR errnum myerrnum;
    PROC main()
        
        
		CONST robtarget p10:=[[-9.58,182.61,198.63],[0.0660087,0.84242,-0.111216,0.52307],[0,0,0,11],[107.816,9E+09,9E+09,9E+09,9E+09,9E+09]];
	!	MoveL p20, v5000, z50, tool0;
	p20:=CRobT(\Tool:=tool0 \WObj:=wobj0);	
    p30 := p20;
		WHILE TRUE DO
          !  IF (NOT P20_X = 0) AND (NOT P20_Y = 0) AND (NOT P20_Z = 0) THEN
            !p30 := Offs(p30,P20_X,P20_X,P20_X);
!			jointpos1 := CalcJointT(p30, tool0 \WObj:=wobj0 \ErrorNumber:=myerrnum);
 
IF NOT myerrnum = 0 THEN

TPWrite "Joint jointpos3 can not be reached.";

TPWrite "jointpos3.robax.rax_1: "+ValToStr(jointpos1.robax.rax_1);

TPWrite "jointpos3.extax.eax_f"+ValToStr(jointpos1.extax.eax_f);

p30 := p20;
 MoveL p30, v5000, z50, tool0;    
 
ELSE
            MoveJ p30, v5000, fine, tool0;    

ENDIF


            P20_X := 0;
			P20_Y := 0;
            P20_Z := 0;
        
        ENDWHILE
        
        
        ERROR
            p30 := p20;
            RETRY;
    ENDPROC
ENDMODULE