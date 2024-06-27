start ../../PacketGenerator/bin/PacketGenerator.exe ../../PacketGenerator/PDL.xml

XCOPY /y GenPackets.cs "../../DummyClient/Packet"
XCOPY /y GenPackets.cs "../../Server/Packet"