import socket
import json

HOST = "127.0.0.1"
PORT = 5000


def main():
    print("DBS Application Client")
    name = input("Name: ")
    address = input("Address: ")
    qualifications = input("Educational qualifications: ")
    course = input("Course (MSc Cyber Security / MSc Information Systems & computing / MSc Data Analytics): ")
    start_year = input("Intended start year: ")
    start_month = input("Intended start month (number): ")

    info = {
        "name": name,
        "address": address,
        "qualifications": qualifications,
        "course": course,
        "start_year": start_year,
        "start_month": start_month,
    }

    data = json.dumps(info).encode("utf-8")

    with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
        s.connect((HOST, PORT))
        s.sendall(data)
        reg_number = s.recv(1024).decode("utf-8")

    print("Your registration number is:", reg_number)


if __name__ == "__main__":
    main()
