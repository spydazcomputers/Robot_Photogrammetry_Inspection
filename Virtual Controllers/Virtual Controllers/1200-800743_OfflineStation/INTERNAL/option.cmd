
task -slotname ltdis   -slotid  35 -pri 110 -vwopt 0x1c -stcks 16000 \
-entp ltdists_main -auto -noreg

task -slotname ltdis2  -slotid  53 -pri 110 -vwopt 0x1c -stcks 16000 \
-entp ltdists_main -auto -noreg


task -slotname gsictrlts -slotid 119 -pri 5 -vwopt 0x1c -stcks 8000 \
-entp gsictrlts_main -auto


task -slotname udpuc -slotid 138 -pri 77 -vwopt 0x1c -stcks 20000 \
-entp udpuc_main -auto -noreg
