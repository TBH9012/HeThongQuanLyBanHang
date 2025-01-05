	CREATE DATABASE QUANLYBANHANG
	use QUANLYBANHANG

	CREATE TABLE tblChatlieu (
		MaChatlieu INT PRIMARY KEY,
		TenChatlieu NVARCHAR(100) NOT NULL
	);
	CREATE TABLE tblHang (
		MaHang INT PRIMARY KEY,
		TenHang NVARCHAR(100) NOT NULL,
		MaChatlieu INT NOT NULL,
		SoLuong INT DEFAULT 0,
		DonGiaNhap DECIMAL(18, 2),
		DonGiaBan DECIMAL(18, 2),
		Anh NVARCHAR(255),
		GhiChu NVARCHAR(255),
		FOREIGN KEY (MaChatlieu) REFERENCES tblChatlieu(MaChatlieu)
	);
	CREATE TABLE tblKhach (
		MaKhach INT PRIMARY KEY,
		TenKhach NVARCHAR(100) NOT NULL,
		DiaChi NVARCHAR(255),
		DienThoai NVARCHAR(15)
	);
	CREATE TABLE tblNhanvien (
		MaNhanvien INT PRIMARY KEY,
		TenNhanvien NVARCHAR(100) NOT NULL,
		GioiTinh NVARCHAR(10),
		DiaChi NVARCHAR(255),
		DienThoai NVARCHAR(15),
		NgaySinh DATE
	);
	CREATE TABLE tblHDBan (
		MaHDBan INT PRIMARY KEY,
		MaNhanvien INT NOT NULL,
		NgayBan DATE NOT NULL,
		MaKhach INT NOT NULL,
		TongTien DECIMAL(18, 2),
		FOREIGN KEY (MaNhanvien) REFERENCES tblNhanvien(MaNhanvien),
		FOREIGN KEY (MaKhach) REFERENCES tblKhach(MaKhach)
	);
	CREATE TABLE tblChitietHDBan (
		MaHDBan INT NOT NULL,
		MaHang INT NOT NULL,
		SoLuong INT NOT NULL,
		DonGia DECIMAL(18, 2),
		GiamGia DECIMAL(18, 2),
		ThanhTien DECIMAL(18, 2),
		PRIMARY KEY (MaHDBan, MaHang),
		FOREIGN KEY (MaHDBan) REFERENCES tblHDBan(MaHDBan),
		FOREIGN KEY (MaHang) REFERENCES tblHang(MaHang)
	);

	INSERT INTO tblChatlieu (MaChatlieu, TenChatlieu) VALUES
	(1, N'Vải Cotton'),
	(2, N'Vải Linen'),
	(3, N'Vải Polyester'),
	(4, N'Vải Jeans'),
	(5, N'Vải Nỉ'),
	(6, N'Vải Lụa'),
	(7, N'Vải Thun'),
	(8, N'Vải Da'),
	(9, N'Vải Mỏng'),
	(10, N'Vải Dày');

	INSERT INTO tblHang (MaHang, TenHang, MaChatlieu, SoLuong, DonGiaNhap, DonGiaBan, Anh, GhiChu) VALUES
	(1, N'T-shirt', 1, 100, 100000, 150000, N'images/tshirt.jpg', N'Mẫu t-shirt cotton'),
	(2, N'Shirt', 2, 50, 120000, 180000, N'images/shirt.jpg', N'Mẫu áo sơ mi linen'),
	(3, N'Sweater', 5, 30, 200000, 250000, N'images/sweater.jpg', N'Mẫu áo nỉ'),
	(4, N'Jeans', 4, 80, 250000, 350000, N'images/jeans.jpg', N'Mẫu quần jeans'),
	(5, N'Dress', 1, 40, 150000, 220000, N'images/dress.jpg', N'Mẫu váy cotton'),
	(6, N'Skirt', 3, 60, 130000, 180000, N'images/skirt.jpg', N'Mẫu chân váy polyester'),
	(7, N'Jacket', 2, 70, 300000, 400000, N'images/jacket.jpg', N'Mẫu áo khoác linen'),
	(8, N'Blouse', 6, 45, 170000, 250000, N'images/blouse.jpg', N'Mẫu blouse lụa'),
	(9, N'Top', 7, 90, 80000, 120000, N'images/top.jpg', N'Mẫu áo thun'),
	(10, N'Coat', 8, 20, 500000, 650000, N'images/coat.jpg', N'Mẫu áo khoác da');

	INSERT INTO tblKhach (MaKhach, TenKhach, DiaChi, DienThoai) VALUES
	(1, N'Nguyễn Văn A', N'Hà Nội', N'0912345678'),
	(2, N'Trần Thị B', N'TP.HCM', N'0987654321'),
	(3, N'Phạm Minh C', N'Đà Nẵng', N'0912345679'),
	(4, N'Nguyễn Thị D', N'Bắc Ninh', N'0987654322'),
	(5, N'Trần Văn E', N'Hải Phòng', N'0912345680'),
	(6, N'Phan Minh F', N'Vũng Tàu', N'0987654323'),
	(7, N'Ngô Thị G', N'Quảng Ninh', N'0912345681'),
	(8, N'Trần Minh H', N'Quảng Ngãi', N'0987654324'),
	(9, N'Nguyễn Minh I', N'Bắc Giang', N'0912345682'),
	(10, N'Phan Thị K', N'Quảng Nam', N'0987654325');

	INSERT INTO tblNhanvien (MaNhanvien, TenNhanvien, GioiTinh, DiaChi, DienThoai, NgaySinh) VALUES
	(1, N'Nguyễn Đức T', N'Nam', N'Hà Nội', N'0912345678', '1990-01-01'),
	(2, N'Trần Minh Q', N'Nữ', N'TP.HCM', N'0987654321', '1992-02-14'),
	(3, N'Phạm Văn H', N'Nam', N'Đà Nẵng', N'0912345679', '1988-05-20'),
	(4, N'Nguyễn Thị A', N'Nữ', N'Bắc Ninh', N'0987654322', '1995-06-25'),
	(5, N'Trần Minh B', N'Nam', N'Hải Phòng', N'0912345680', '1993-08-10'),
	(6, N'Phan Minh C', N'Nam', N'Vũng Tàu', N'0987654323', '1994-03-15'),
	(7, N'Ngô Thị M', N'Nữ', N'Quảng Ninh', N'0912345681', '1996-09-30'),
	(8, N'Trần Minh D', N'Nam', N'Quảng Ngãi', N'0987654324', '1991-11-20'),
	(9, N'Nguyễn Minh G', N'Nam', N'Bắc Giang', N'0912345682', '1987-12-05'),
	(10, N'Phan Thị L', N'Nữ', N'Quảng Nam', N'0987654325', '1992-04-18');

	INSERT INTO tblHDBan (MaHDBan, MaNhanvien, NgayBan, MaKhach, TongTien) VALUES
	(1, 1, '2024-12-10', 1, 1000000),
	(2, 2, '2024-12-11', 2, 1500000),
	(3, 3, '2024-12-12', 3, 2000000),
	(4, 4, '2024-12-13', 4, 2500000),
	(5, 5, '2024-12-14', 5, 3000000),
	(6, 6, '2024-12-15', 6, 3500000),
	(7, 7, '2024-12-16', 7, 4000000),
	(8, 8, '2024-12-17', 8, 4500000),
	(9, 9, '2024-12-18', 9, 5000000),
	(10, 10, '2024-12-19', 10, 5500000);

	INSERT INTO tblChitietHDBan (MaHDBan, MaHang, SoLuong, DonGia, GiamGia, ThanhTien) VALUES
	(1, 1, 2, 150000, 10000, 290000),
	(2, 2, 3, 180000, 20000, 460000),
	(3, 3, 1, 250000, 30000, 220000),
	(4, 4, 2, 350000, 50000, 600000),
	(5, 5, 3, 220000, 15000, 615000),
	(6, 6, 1, 250000, 10000, 240000),
	(7, 7, 2, 180000, 5000, 355000),
	(8, 8, 1, 250000, 15000, 235000),
	(9, 9, 2, 120000, 10000, 230000),
	(10, 10, 3, 200000, 25000, 545000);

