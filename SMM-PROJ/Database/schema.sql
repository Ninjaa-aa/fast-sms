-- ============================================================
-- Societies Management System — Database Schema
-- Target: SQL Server (LocalDB or Express)
-- ============================================================

CREATE DATABASE SocietiesManagementSystem;
GO

USE SocietiesManagementSystem;
GO

-- ======================
-- Users table (all roles)
-- ======================
CREATE TABLE Users (
    UserID      INT PRIMARY KEY IDENTITY(1,1),
    FullName    NVARCHAR(100)  NOT NULL,
    Email       NVARCHAR(100)  UNIQUE NOT NULL,
    PasswordHash NVARCHAR(256) NOT NULL,
    Role        NVARCHAR(20)   NOT NULL
                CHECK (Role IN ('Student', 'SocietyHead', 'Admin')),
    CreatedAt   DATETIME       DEFAULT GETDATE()
);

-- ======================
-- Societies table
-- ======================
CREATE TABLE Societies (
    SocietyID   INT PRIMARY KEY IDENTITY(1,1),
    Name        NVARCHAR(100)  NOT NULL,
    Description NVARCHAR(500),
    HeadUserID  INT            NOT NULL
                FOREIGN KEY REFERENCES Users(UserID),
    Status      NVARCHAR(20)   DEFAULT 'Pending'
                CHECK (Status IN ('Pending', 'Active', 'Suspended')),
    CreatedAt   DATETIME       DEFAULT GETDATE()
);

-- ======================
-- Memberships (Student <-> Society)
-- ======================
CREATE TABLE Memberships (
    MembershipID INT PRIMARY KEY IDENTITY(1,1),
    UserID       INT NOT NULL FOREIGN KEY REFERENCES Users(UserID),
    SocietyID    INT NOT NULL FOREIGN KEY REFERENCES Societies(SocietyID),
    Status       NVARCHAR(20) DEFAULT 'Pending'
                 CHECK (Status IN ('Pending', 'Approved', 'Rejected')),
    AppliedAt    DATETIME     DEFAULT GETDATE(),
    ApprovedAt   DATETIME,
    CONSTRAINT UQ_Membership UNIQUE (UserID, SocietyID)
);

-- ======================
-- Events table
-- ======================
CREATE TABLE Events (
    EventID     INT PRIMARY KEY IDENTITY(1,1),
    SocietyID   INT NOT NULL FOREIGN KEY REFERENCES Societies(SocietyID),
    Title       NVARCHAR(150) NOT NULL,
    Description NVARCHAR(1000),
    EventDate   DATETIME      NOT NULL,
    Venue       NVARCHAR(200),
    Status      NVARCHAR(20)  DEFAULT 'Pending'
                CHECK (Status IN ('Pending', 'Approved', 'Cancelled')),
    CreatedAt   DATETIME      DEFAULT GETDATE()
);

-- ======================
-- Event Registrations (Student registers for an event)
-- ======================
CREATE TABLE EventRegistrations (
    RegistrationID INT PRIMARY KEY IDENTITY(1,1),
    EventID        INT NOT NULL FOREIGN KEY REFERENCES Events(EventID),
    UserID         INT NOT NULL FOREIGN KEY REFERENCES Users(UserID),
    TicketCode     NVARCHAR(50) UNIQUE,
    RegisteredAt   DATETIME     DEFAULT GETDATE(),
    CONSTRAINT UQ_EventRegistration UNIQUE (EventID, UserID)
);

-- ======================
-- Tasks (Society assigns to members)
-- ======================
CREATE TABLE Tasks (
    TaskID      INT PRIMARY KEY IDENTITY(1,1),
    SocietyID   INT NOT NULL FOREIGN KEY REFERENCES Societies(SocietyID),
    AssignedTo  INT NOT NULL FOREIGN KEY REFERENCES Users(UserID),
    AssignedBy  INT NOT NULL FOREIGN KEY REFERENCES Users(UserID),
    Title       NVARCHAR(150) NOT NULL,
    Description NVARCHAR(500),
    DueDate     DATETIME,
    Status      NVARCHAR(20)  DEFAULT 'Pending'
                CHECK (Status IN ('Pending', 'InProgress', 'Completed')),
    CreatedAt   DATETIME      DEFAULT GETDATE()
);

-- ======================
-- Announcements
-- ======================
CREATE TABLE Announcements (
    AnnouncementID INT PRIMARY KEY IDENTITY(1,1),
    SocietyID      INT NOT NULL FOREIGN KEY REFERENCES Societies(SocietyID),
    Title          NVARCHAR(150) NOT NULL,
    Content        NVARCHAR(2000),
    PostedAt       DATETIME      DEFAULT GETDATE()
);
