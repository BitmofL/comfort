-- Database schema for product management subsystem

-- Table for Workshops (Цеха)
CREATE TABLE Workshops (
    WorkshopID INT IDENTITY(1,1) PRIMARY KEY,
    WorkshopName NVARCHAR(100) NOT NULL
);

-- Table for Product Types (Типы продукции)
CREATE TABLE ProductTypes (
    ProductTypeID INT IDENTITY(1,1) PRIMARY KEY,
    TypeName NVARCHAR(100) NOT NULL
);

-- Table for Products (Продукция)
CREATE TABLE Products (
    ProductID INT IDENTITY(1,1) PRIMARY KEY,
    Article NVARCHAR(50) NOT NULL,
    ProductTypeID INT NOT NULL,
    ProductName NVARCHAR(200) NOT NULL,
    MinPartnerPrice DECIMAL(18,2) NOT NULL CHECK (MinPartnerPrice >= 0),
    MainMaterial NVARCHAR(200) NOT NULL,
    ManufacturingTime INT NOT NULL CHECK (ManufacturingTime >= 0),
    WorkshopID INT NOT NULL,
    FOREIGN KEY (ProductTypeID) REFERENCES ProductTypes(ProductTypeID),
    FOREIGN KEY (WorkshopID) REFERENCES Workshops(WorkshopID)
);

-- Table for Product_Workshops (Время нахождения продукции в каждом цехе)
CREATE TABLE ProductWorkshops (
    ProductWorkshopID INT IDENTITY(1,1) PRIMARY KEY,
    ProductID INT NOT NULL,
    WorkshopID INT NOT NULL,
    TimeInWorkshop INT NOT NULL CHECK (TimeInWorkshop >= 0),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID),
    FOREIGN KEY (WorkshopID) REFERENCES Workshops(WorkshopID)
);

-- Additional tables can be added as needed

-- Sample data insertion can be done after import preparation
