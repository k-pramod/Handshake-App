import smbus
import math
import time
import requests

#Addresses on MPU6050 board to read data
gyro_x_addr = 0x43
gyro_y_addr = 0x45
gyro_z_addr = 0x47
accel_x_addr = 0x3B
accel_y_addr = 0x3D
accel_z_addr = 0x3F

def read(addr):
    high = bus.read_byte_data(address, addr)
    low = bus.read_byte_data(address, addr+1)
    val = (high << 8) + low
    return val

def read_data(addr):
    val = read(addr)
    if (val >= 0x8000):
        return -((65535 - val) + 1)
    else:
        return val
    
#Math to determine yaw and pitch based on measurements
def dist(a,b,c):
    return math.sqrt((a*a) + (b*b) + (c*c))

def rotX(x,y,z): #Roll
    radians = math.atan2(y, dist(x,z))
    return math.degrees(radians)

def rotY(x,y,z): #Pitch
    radians = math.atan2(x, dist(y,z))
    return -math.degrees(radians)

def set_data():
    global gyrox
    global gyroy
    global gyroz
    global accelx
    global accely
    global accelz
    gyrox = read_data(gyro_x_addr) / 131
    gyroy = read_data(gyro_y_addr) / 131
    gyroz = read_data(gyro_z_addr) / 131
    accelx = read_data(accel_x_addr) / 16384.0
    accely = read_data(accel_y_addr) / 16384.0
    accelz = read_data(accel_z_addr) / 16384.0
    return

def compFilter(): #Complementary Filter, combines accelerometer and gyroscope data
    global dt
    global Xangle
    global Yangle
    dt = 0.005
    Xangle = 0.98 * (rotX(accelx,accely,accelz) + gyrox * dt) + 0.02 * accelx
    Yangle = 0.98 * (rotY(accelx,accely,accelz) + gyroy * dt) + 0.02 * accely
    
bus = smbus.SMBus(1)
address = 0x68
bus.write_byte_data(address, 0x6b, 0)

set_data()
        

poshandshakeDetect = 0
neghandshakeDetect = 0
handshakeAcc = False

count = 0
while True:
	set_data()       
	accelTot = dist(accelx,accely,accelz) 
	gyroTot = dist(gyrox,gyroy,gyroz)
	
	if accelTot > 2.5:
		handshakeAcc = True
	if handshakeAcc == True:
		count = count + 1
		if accelTot > 2:
			poshandshakeDetect += 1
		if accelTot < 0.8:
			neghandshakeDetect += 1
	if (count < 400) & (handshakeAcc == True) & (poshandshakeDetect > 21) & (neghandshakeDetect > 4):
		print("HANDSHAKE DETECTED!")
		r = requests.post("http://handshake.azurewebsites.net/Handshakes/Log",data={"userId":1010201})
		break
	if count > 400:
		count = 0
		handshakeAcc = False 
	print accelTot, gyroTot, count, handshakeAcc, poshandshakeDetect, neghandshakeDetect	


