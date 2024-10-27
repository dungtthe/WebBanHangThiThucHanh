INSERT INTO Roles (RoleName) VALUES
(N'Admin'),
(N'Customer');


INSERT INTO AppUsers (UserName, Password, Email, PhoneNumber, RoleId) VALUES
('admin', '123456', NULL, NULL, 1),
('khachhang', '123456', NULL, NULL, 2);

