-- ============================================================
-- Societies Management System — Seed Data
-- ============================================================
-- All passwords are 'pass123' hashed with BCrypt.
-- BCrypt hash for 'pass123': $2a$11$OxIgT0/KvyQzqxsYehv4euB/Mi3/XDDVJfvie3XzLSL3lwQlznfY

USE SocietiesManagementSystem;
GO

-- ======================
-- Users: 1 Admin, 2 Society Heads, 3 Students
-- ======================
INSERT INTO Users (FullName, Email, PasswordHash, Role) VALUES
('System Admin',  'admin@fast.edu.pk',   '$2a$11$OxIgT0/KvyQzqxsYehv4euB/Mi3/XDDVJfvie3XzLSL3lwQlznfY', 'Admin'),
('Ali Hassan',    'ali@fast.edu.pk',     '$2a$11$OxIgT0/KvyQzqxsYehv4euB/Mi3/XDDVJfvie3XzLSL3lwQlznfY', 'SocietyHead'),
('Sara Khan',     'sara@fast.edu.pk',    '$2a$11$OxIgT0/KvyQzqxsYehv4euB/Mi3/XDDVJfvie3XzLSL3lwQlznfY', 'SocietyHead'),
('Ahmed Raza',    'ahmed@fast.edu.pk',   '$2a$11$OxIgT0/KvyQzqxsYehv4euB/Mi3/XDDVJfvie3XzLSL3lwQlznfY', 'Student'),
('Fatima Malik',  'fatima@fast.edu.pk',  '$2a$11$OxIgT0/KvyQzqxsYehv4euB/Mi3/XDDVJfvie3XzLSL3lwQlznfY', 'Student'),
('Usman Tariq',   'usman@fast.edu.pk',   '$2a$11$OxIgT0/KvyQzqxsYehv4euB/Mi3/XDDVJfvie3XzLSL3lwQlznfY', 'Student');

-- ======================
-- Societies (HeadUserID: 2 = Ali, 3 = Sara)
-- ======================
INSERT INTO Societies (Name, Description, HeadUserID, Status) VALUES
('Gaming Society',    'For gaming enthusiasts',           2, 'Active'),
('Developers Club',   'For software developers',          3, 'Active'),
('Literary Society',  'For book lovers and writers',      2, 'Pending');

-- ======================
-- Memberships
-- ======================
INSERT INTO Memberships (UserID, SocietyID, Status, ApprovedAt) VALUES
(4, 1, 'Approved', GETDATE()),
(5, 1, 'Pending',  NULL),
(6, 2, 'Approved', GETDATE()),
(4, 2, 'Rejected', NULL);

-- ======================
-- Events
-- ======================
INSERT INTO Events (SocietyID, Title, Description, EventDate, Venue, Status) VALUES
(1, 'Gaming Tournament 2025', 'Annual gaming competition',     '2025-06-15', 'CS Lab 1',    'Approved'),
(2, 'Hackathon Spring',       '24-hour coding competition',    '2025-07-01', 'Auditorium',  'Pending'),
(1, 'LAN Party',              'Casual LAN gaming night',       '2025-08-10', 'CS Lab 2',    'Approved');

-- ======================
-- Event Registrations
-- ======================
INSERT INTO EventRegistrations (EventID, UserID, TicketCode) VALUES
(1, 4, 'TKT-A1B2C3D4'),
(1, 6, 'TKT-E5F6G7H8');

-- ======================
-- Tasks
-- ======================
INSERT INTO Tasks (SocietyID, AssignedTo, AssignedBy, Title, Description, DueDate, Status) VALUES
(1, 4, 2, 'Design Tournament Poster', 'Create a poster for the gaming tournament', '2025-06-01', 'Completed'),
(2, 6, 3, 'Setup GitHub Repo',        'Create the project repo for hackathon',      '2025-06-20', 'Pending');

-- ======================
-- Announcements
-- ======================
INSERT INTO Announcements (SocietyID, Title, Content) VALUES
(1, 'Welcome to Gaming Society!',   'We are excited to have you. Stay tuned for upcoming events.'),
(2, 'Hackathon Registration Open',  'Register now for the Spring Hackathon. Limited slots available!');
