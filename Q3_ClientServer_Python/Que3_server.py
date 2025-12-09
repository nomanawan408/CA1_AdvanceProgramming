import socket
import sqlite3
import json

DB_NAME = "applications.db"
HOST = "127.0.0.1"
PORT = 5000


def init_db():
    conn = sqlite3.connect(DB_NAME)
    cur = conn.cursor()
    cur.execute(
        """
        CREATE TABLE IF NOT EXISTS applications (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            name TEXT,
            address TEXT,
            qualifications TEXT,
            course TEXT,
            start_year INTEGER,
            start_month INTEGER
        )
        """
    )
    conn.commit()
    conn.close()


def main():
    init_db()

    with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
        s.bind((HOST, PORT))
        s.listen(1)
        print(f"Server listening on {HOST}:{PORT}")

        while True:
            conn, addr = s.accept()
            with conn:
                print("Connected by", addr)
                data = conn.recv(4096)
                if not data:
                    continue

                try:
                    info = json.loads(data.decode("utf-8"))
                except json.JSONDecodeError:
                    conn.sendall(b"ERROR: invalid data")
                    continue

                reg_number = save_application(info)
                conn.sendall(reg_number.encode("utf-8"))


def save_application(info):
    conn = sqlite3.connect(DB_NAME)
    cur = conn.cursor()
    cur.execute(
        "INSERT INTO applications (name, address, qualifications, course, start_year, start_month) VALUES (?, ?, ?, ?, ?, ?)",
        (
            info.get("name", ""),
            info.get("address", ""),
            info.get("qualifications", ""),
            info.get("course", ""),
            int(info.get("start_year", 0) or 0),
            int(info.get("start_month", 0) or 0),
        ),
    )
    conn.commit()
    app_id = cur.lastrowid
    conn.close()
    reg_number = f"DBS-{app_id:05d}"
    return reg_number


if __name__ == "__main__":
    main()
