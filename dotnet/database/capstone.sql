USE master
GO

--drop database if it exists
IF DB_ID('final_capstone') IS NOT NULL
BEGIN
	ALTER DATABASE final_capstone SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE final_capstone;
END

CREATE DATABASE final_capstone
GO

USE final_capstone
GO

--create tables
CREATE TABLE users (
	user_id int IDENTITY(1,1) NOT NULL,
	username varchar(50) NOT NULL,
	password_hash varchar(200) NOT NULL,
	salt varchar(200) NOT NULL,
	user_role varchar(50) NOT NULL
	CONSTRAINT PK_user PRIMARY KEY (user_id)
)

--pothole table
CREATE TABLE potholes (
	pothole_id int IDENTITY(1,1) NOT NULL,
	latitude decimal(8,6) NOT NULL,
	longitude decimal(9,6) NOT NULL,
	image_link varchar(300),
	reported_date DATE NOT NULL,
	reporting_user_id int NOT NULL,
	inspected_date DATE,
	repaired_date DATE,
	repair_status varchar(50),
	severity TINYINT,
		CONSTRAINT between_one_and_ten CHECK(severity >=1 AND severity <= 10),
	CONSTRAINT PK_potholes PRIMARY KEY (pothole_id),
	CONSTRAINT FK_user_to_pothole FOREIGN KEY (reporting_user_id) REFERENCES users(user_id)
)

-- Requests for users to become employees table
CREATE TABLE requests (
	request_id int IDENTITY(1,1) NOT NULL,
	user_id int NOT NULL,
	active_status bit NOT NULL,
	CONSTRAINT PK_requests PRIMARY KEY (request_id),
	CONSTRAINT FK_requests_to_users FOREIGN KEY (user_id) REFERENCES users(user_id)
)

--Claim forms
CREATE TABLE claims (
	claim_id INT IDENTITY(1,1) NOT NULL,
	user_id INT NOT NULL,
	first_name VARCHAR(100) NOT NULL,
	last_name VARCHAR(100) NOT NULL,
	email VARCHAR(345) NOT NULL,
	phone_number VARCHAR(20) NOT NULL,
	date_of_claim DATE NOT NULL,
	date_of_incident DATE NOT NULL,
	location_of_incident_city VARCHAR(120) NOT NULL,
	location_of_incident_state VARCHAR(120) NOT NULL,
	image_link VARCHAR(1200),
	description_of_damage VARCHAR(1200) NOT NULL,
	claim_status VARCHAR (30) NOT NULL DEFAULT 'Pending',
	CONSTRAINT PK_claims PRIMARY KEY (claim_id),
	CONSTRAINT FK_claims_to_users FOREIGN KEY (user_id) REFERENCES users(user_id)
)

--populate default data
INSERT INTO users (username, password_hash, salt, user_role) VALUES 
('user','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user'),
('admin','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin'),
('employee','+evA40KNSwa/H67KO3dyrIGCRCc=','ERHtn4EGU9k=','employee'),
('rejected','bJpe3BopB1nJs94HePre1Gchktw=','v5wtylq92QU=','user'),
('pending1','ppX+UEfBTBjHB9oHBKt4hsIjHcM=','670msKfgokE=','user'),
('pending2','V2CyqQMj78DWAhvPV1e/iJ9RN6o=','pbcLS6SbOqg=','user'),
('pending3','IAM7JZf3uYcDVIGKrJBr388Wonc=','X3i9WS023/o=','user');

--ONE DATE
INSERT INTO potholes(latitude,longitude,image_link,reported_date, reporting_user_id, repair_status) VALUES 
(39.159490, -84.455277, 'https://sfpublicworks.org/sites/default/files/texas-pothole-635x424.jpg','8/2/2021',1, 'Reported'),
(39.129751, -84.474612, 'https://upload.wikimedia.org/wikipedia/commons/thumb/1/1e/Pothole%2C_Vas%C3%BAt_utca%2C_Zichy%C3%BAjfalu_001.jpg/800px-Pothole%2C_Vas%C3%BAt_utca%2C_Zichy%C3%BAjfalu_001.jpg','8/2/2021',1, 'Reported'),
(39.138013, -84.487662,'https://upload.wikimedia.org/wikipedia/commons/thumb/0/03/Clausius_crater_4148_h2_h3.jpg/600px-Clausius_crater_4148_h2_h3.jpg','8/7/2021',1, 'Reported');

--TWO DATES
INSERT INTO potholes(latitude,longitude,image_link,reported_date, inspected_date, reporting_user_id, repair_status, severity) VALUES 
(39.145706, -84.527141, 'https://www.kttn.com/wp-content/uploads/2019/02/Pothole.jpg','8/2/2021','8/5/2021',2, 'Inspected',1),
(39.1529770, -84.477101, 'https://kdvr.com/wp-content/uploads/sites/11/2019/03/gettyimages-183851840.jpg','8/2/2021','8/9/2021',3, 'Inspected',2),
(39.135546, -84.512969, 'https://upload.wikimedia.org/wikipedia/commons/thumb/2/22/A_Pothole_Is_Decorated_-_Mid-City_New_Orleans.jpg/800px-A_Pothole_Is_Decorated_-_Mid-City_New_Orleans.jpg','8/7/2021','8/20/2021',2, 'Reported',2),
(39.112670, -84.512436,'https://upload.wikimedia.org/wikipedia/commons/thumb/b/b3/Pothole_in_an_asphalt_pavement.jpg/800px-Pothole_in_an_asphalt_pavement.jpg', '8/1/2021','8/5/2021',3, 'Inspected',3),
(39.133851, -84.413645,'https://upload.wikimedia.org/wikipedia/commons/thumb/8/84/Marked_Pothole.jpg/640px-Marked_Pothole.jpg','8/3/2021','8/4/2021',2, 'Inspected',2),
(39.101217, -84.512969,'https://upload.wikimedia.org/wikipedia/commons/thumb/9/91/Nuuksio_pothole.jpg/800px-Nuuksio_pothole.jpg','8/5/2021','8/6/2021',3, 'Inspected',3);

--THREE DATES
INSERT INTO potholes(latitude,longitude,image_link,reported_date, inspected_date, repaired_date, reporting_user_id, repair_status, severity) VALUES 
(39.109466, -84.515063,'https://upload.wikimedia.org/wikipedia/commons/c/c7/Pothole_Big.jpg','8/2/2021','8/3/2021','8/11/2021',1, 'Repaired',2),
(39.108425, -84.536468,'https://upload.wikimedia.org/wikipedia/commons/thumb/5/5e/Sinkhole.jpg/800px-Sinkhole.jpg','8/6/2021','8/7/2021','8/8/2021', 4, 'Repaired',1),
(39.147452, -84.452161, 'https://media1.fdncms.com/chicago/imager/u/original/71086724/potholes010.jpg','8/2/2021','8/4/2021','8/11/2021',3, 'Repaired',1),
(39.158086, -84.463604, 'https://www.thebalance.com/thmb/VlnrT3pRKvtegoumE0fXWmA4pWI=/2121x1193/smart/filters:no_upscale()/pothole-174662203-5a7dc84aae9ab80036c6ad36.jpg','8/2/2021','8/3/2021','8/5/2021',4, 'Repaired',2),
(38.299890, -83.175415,'https://upload.wikimedia.org/wikipedia/commons/thumb/7/79/Gold_fish_in_a_pothole_11.jpg/800px-Gold_fish_in_a_pothole_11.jpg','8/1/2021', '8/2/2021','8/3/2021',2, 'Repaired',3);

INSERT INTO requests (user_id, active_status) VALUES 
(1, 1),
(5, 1),
(6, 1),
(7, 1);

INSERT INTO claims (user_id, first_name, last_name, email, phone_number, date_of_claim, date_of_incident, location_of_incident_city, location_of_incident_state, image_link, description_of_damage) VALUES 
(1, 'Bob', 'Bobertson', 'bob@aol.com', '123-456-7890', '2021-08-10', '2021-08-03', 'Cincinnati', 'OH', 'https://www.gannett-cdn.com/presto/2019/02/22/PDTF/b7c4a04f-7f1a-44ac-b689-9516f1b7c7e5-52365689_2576383472378953_4680490136083890176_n.jpg?auto=webp&crop=719,404,x1,y190&format=pjpg&width=1200', 'My car is all messed up!'),
(2, 'Violet', 'Baudelaire', 'series@unfortunateevents.com', '444-444-4444', '2021-08-05', '2021-08-01', 'Miserable Mill', 'MN', 'https://upload.wikimedia.org/wikipedia/commons/thumb/5/58/Car_wreck_Memphis_TN_2013-11--04_004.jpg/799px-Car_wreck_Memphis_TN_2013-11--04_004.jpg', 'What a pothole!'),
(3, 'Artemis', 'Fowl', 'fowl@fowlmanor.com', '555-455-6555', '2021-08-05', '2021-08-01', 'New York City', 'NY', 'https://upload.wikimedia.org/wikipedia/commons/thumb/1/11/Crashed_car_in_Helsinki.JPG/800px-Crashed_car_in_Helsinki.JPG', 'You WILL pay for this');
GO
