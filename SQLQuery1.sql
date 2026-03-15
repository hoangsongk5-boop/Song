
use Quanlysinhvien
go
if OBJECT_ID('Sinhvien' , 'U') is not null
drop table Sinhvien
go
if OBJECT_ID('Lophoc', 'U') is not null
drop table Lophoc
go
create table Lophoc
(
Malop nvarchar(20) primary key,
Tenlop nvarchar(100),
Khoa nvarchar(100),
Chuyennganh nvarchar(100),
Soluongsinhvien int
)
go
create table Sinhvien
(
MaSv nvarchar(20) primary key,
Hoten nvarchar(100),
Ngaysinh date,
Chuyennganh nvarchar(100),
Gioitinh nvarchar(20),
Tenlop nvarchar(20)
)
go

INSERT INTO Lophoc(Tenlop, Malop, Khoa, Chuyennganh, Soluongsinhvien)
VALUES ('68IT4', '0023668', 'cntt', 'IT', '100');
go

INSERT INTO Sinhvien(MaSv, Hoten, Ngaysinh, Chuyennganh, Gioitinh, Tenlop)
VALUES ('000206', 'Song', '02/06/2005', 'IT', 'Nam', '68IT4');