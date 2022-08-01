import socket
import time

s = socket.socket()


host = "127.0.0.1" # We set the local ip address as the ip address
port = 25001 # We wrote the port we want to use

try:
   
    s.connect((host, port)) # We established our connection

except socket.error as msg:
    print("[Something went wrong. Message: ", msg)

i = 0

while True:
    time.sleep(0.5) # We wanted it to send 2 data per second
    i +=1 #increase x by one
    print(i)

    s.sendall(i.encode("UTF-8")) # Converting string to Byte, and sending it to C#
    receivedData = s.recv(1024).decode("UTF-8") # Receiveing data in Byte fron C#, and converting it to String
    print(receivedData)
