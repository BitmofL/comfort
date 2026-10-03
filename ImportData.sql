-- Создание таблиц для импорта данных

-- Таблица типов материалов
IF OBJECT_ID('MaterialTypes', 'U') IS NOT NULL DROP TABLE MaterialTypes;
CREATE TABLE MaterialTypes (
    MaterialTypeID INT IDENTITY(1,1) PRIMARY KEY,
    MaterialType NVARCHAR(100) NOT NULL,
    LossPercentage FLOAT NOT NULL
);

-- Таблица типов продукции
IF OBJECT_ID('ProductTypes', 'U') IS NOT NULL DROP TABLE ProductTypes;
CREATE TABLE ProductTypes (
    ProductTypeID INT IDENTITY(1,1) PRIMARY KEY,
    TypeName NVARCHAR(100) NOT NULL,
    TypeCoefficient FLOAT NOT NULL
);

-- Таблица цехов
IF OBJECT_ID('Workshops', 'U') IS NOT NULL DROP TABLE Workshops;
CREATE TABLE Workshops (
    WorkshopID INT IDENTITY(1,1) PRIMARY KEY,
    WorkshopName NVARCHAR(200) NOT NULL,
    WorkshopType NVARCHAR(100) NOT NULL,
    WorkersCount INT NOT NULL
);

-- Таблица продукции
IF OBJECT_ID('Products', 'U') IS NOT NULL DROP TABLE Products;
CREATE TABLE Products (
    ProductID INT IDENTITY(1,1) PRIMARY KEY,
    ProductTypeID INT NOT NULL,
    ProductName NVARCHAR(200) NOT NULL,
    Article NVARCHAR(50) NOT NULL,
    MinPartnerPrice DECIMAL(18,2) NOT NULL CHECK (MinPartnerPrice >= 0),
    MainMaterial NVARCHAR(200) NOT NULL,
    FOREIGN KEY (ProductTypeID) REFERENCES ProductTypes(ProductTypeID)
);

-- Таблица связи продукции и цехов с временем нахождения
IF OBJECT_ID('ProductWorkshops', 'U') IS NOT NULL DROP TABLE ProductWorkshops;
CREATE TABLE ProductWorkshops (
    ProductWorkshopID INT IDENTITY(1,1) PRIMARY KEY,
    ProductID INT NOT NULL,
    WorkshopID INT NOT NULL,
    TimeInWorkshop INT NOT NULL CHECK (TimeInWorkshop >= 0),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID),
    FOREIGN KEY (WorkshopID) REFERENCES Workshops(WorkshopID)
);

-- Вставка данных

-- Import data for MaterialTypes
INSERT INTO MaterialTypes (MaterialType, LossPercentage) VALUES
('Мебельный щит из массива дерева', 0.008),
('Ламинированное ДСП', 0.007),
('Фанера', 0.0055),
('МДФ', 0.003);

-- Import data for ProductTypes
INSERT INTO ProductTypes (TypeName, TypeCoefficient) VALUES
('Гостиные', 3.5),
('Прихожие', 5.6),
('Мягкая мебель', 3),
('Кровати', 4.7),
('Шкафы', 1.5),
('Комоды', 2.3);

-- Import data for Workshops
INSERT INTO Workshops (WorkshopName, WorkshopType, WorkersCount) VALUES
('Проектный', 'Проектирование', 4),
('Расчетный', 'Проектирование', 5),
('Раскроя', 'Обработка', 5),
('Обработки', 'Обработка', 6),
('Сушильный', 'Сушка', 3),
('Покраски', 'Обработка', 5),
('Столярный', 'Обработка', 7),
('Изготовления изделий из искусственного камня и композитных материалов', 'Обработка', 3),
('Изготовления мягкой мебели', 'Обработка', 5),
('Монтажа стеклянных, зеркальных вставок и других изделий', 'Сборка', 2),
('Сборки', 'Сборка', 6),
('Упаковки', 'Сборка', 4);

-- Import data for Products
INSERT INTO Products (ProductTypeID, ProductName, Article, MinPartnerPrice, MainMaterial) VALUES
-- ProductTypeID should be matched with ProductTypes table IDs
-- Example entries (ProductTypeID to be replaced with actual IDs)
(1, 'Комплект мебели для гостиной Ольха горная', '1549922', 160507.00, 'Мебельный щит из массива дерева'),
(1, 'Стенка для гостиной Вишня темная', '1018556', 216907.00, 'Мебельный щит из массива дерева'),
(2, 'Прихожая Венге Винтаж', '3028272', 24970.00, 'Ламинированное ДСП'),
(2, 'Тумба с вешалкой Дуб натуральный', '3029272', 18206.00, 'Ламинированное ДСП'),
(2, 'Прихожая-комплект Дуб темный', '3028248', 177509.00, 'Мебельный щит из массива дерева'),
(3, 'Диван-кровать угловой Книжка', '7118827', 85900.00, 'Мебельный щит из массива дерева'),
(3, 'Диван модульный Телескоп', '7137981', 75900.00, 'Мебельный щит из массива дерева'),
(3, 'Диван-кровать Соло', '7029787', 120345.00, 'Мебельный щит из массива дерева'),
(3, 'Детский диван Выкатной', '7758953', 25990.00, 'Фанера'),
(4, 'Кровать с подъемным механизмом с матрасом 1600х2000 Венге', '6026662', 69500.00, 'Мебельный щит из массива дерева'),
(4, 'Кровать с матрасом 90х2000 Венге', '6159043', 55600.00, 'Ламинированное ДСП'),
(4, 'Кровать универсальная Дуб натуральный', '6588376', 37900.00, 'Ламинированное ДСП'),
(4, 'Кровать с ящиками Ясень белый', '6758375', 46750.00, 'Фанера'),
(5, 'Шкаф-купе 3-х дверный Сосна белая', '2759324', 131560.00, 'Ламинированное ДСП'),
(5, 'Стеллаж Бук натуральный', '2118827', 38700.00, 'Мебельный щит из массива дерева'),
(5, 'Шкаф 4 дверный с ящиками Ясень серый', '2559898', 160151.00, 'Фанера'),
(5, 'Шкаф-пенал Береза белый', '2259474', 40500.00, 'Фанера'),
(6, 'Комод 6 ящиков Вишня светлая', '4115947', 61235.00, 'Мебельный щит из массива дерева'),
(6, 'Комод 4 ящика Вишня светлая', '4033136', 41200.00, 'Мебельный щит из массива дерева'),
(6, 'Тумба под ТВ', '4028048', 12350.00, 'МДФ');


INSERT INTO ProductWorkshops (ProductID, WorkshopID, TimeInWorkshop) VALUES
(1, 1, 2), (1, 3, 3), (2, 1, 1), (2, 4, 2);

-- Import data for ProductWorkshops (time in workshops)
-- This requires matching ProductID and WorkshopID from previous inserts
-- Example insert:
-- INSERT INTO ProductWorkshops (ProductID, WorkshopID, TimeInWorkshop) VALUES (1, 1, 2.0);

-- Due to complexity, this part should be implemented with a script or program that matches names to IDs.
