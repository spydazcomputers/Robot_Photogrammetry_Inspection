
ifvc -label VC_SKIP_DN1
iomgrinstall -entry dnFBC -name /dnfbc -errlabel ERROR_DNFBC

creat -name /dnfbc/DNET_PCI: -pmode 0 -errlabel ERROR_DNFBC

task -slotname DNread -entp read_ts -pri 72 -vwopt 0x1c -stcks 10000 -nosync -auto
readparam -devicename /DNET_PCI:/bus_read -rmode 1 -buffersize 100
invoke -entry dnfbc_tk_activate -strarg "DNET" -nomode

# Add DeviceNet to system dump service
sysdmp_add -show dnfbc_sysdmp

goto -label VC_SKIP_DN2 

#VC_SKIP_DN1

creat -name /simfbc/DNET_PCI: -pmode 0

#VC_SKIP_DN2
#ERROR_DNFBC

# 
# Note: To run virtual controller with Ethernet/IP using a network interface on PC, deactivate the statement
# ifvc -label VC_SKIP_ENMS below.
#

ifvc -label VC_SKIP_ENMS

iomgrinstall -entry ENIPFBC -name /enipfbc -errlabel FATAL_ERROR_ENIPFBC

creat -name /enipfbc/ENIP_MS: -pmode 0 -errlabel ERROR_ENIP

task -slotname enip_read_ts -entp read_ts -pri 72 -vwopt 0x1c -stcks 10000 -nosync -auto
readparam -devicename /ENIP_MS:/bus_read -rmode 1 -buffersize 100

task -slotname enip_rta_ts -entp rta_main -pri 97 -vwopt 0x0008 -stcks 65000 -nosync -auto -noreg

# Add EtherNet/IP to system dump service
sysdmp_add -show enipfbc_sysdmp

goto -label DONE_ENMS

#VC_SKIP_ENMS

creat -name /simfbc/ENIP_MS: -pmode 0

#ERROR_ENIP
#FATAL_ERROR_ENIPFBC
#DONE_ENMS
