CREATE TABLE livro (
    id INT IDENTITY(1,1) PRIMARY KEY,
    titulo VARCHAR(80) NOT NULL UNIQUE,
    autor VARCHAR(100) NOT NULL,
    editora VARCHAR(100) NOT NULL,
    valor DECIMAL(10,2) NOT NULL,
    isbn VARCHAR(10) NOT NULL UNIQUE,
    fotocapa VARBINARY
);

