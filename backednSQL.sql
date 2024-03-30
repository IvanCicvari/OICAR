IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'Starplex')
BEGIN
    -- Create database
    CREATE DATABASE Starplex;
END
go
USE Starplex;
GO

-- Users Table
-- Users Table
CREATE TABLE Users (
    IDUser INT PRIMARY KEY IDENTITY(1,1),
    first_name NVARCHAR(MAX),
    last_name NVARCHAR(MAX),
    username NVARCHAR(225) UNIQUE,
    email NVARCHAR(255) UNIQUE,
    password_hash NVARCHAR(MAX),  -- Storing hashed password
    password_salt NVARCHAR(MAX),  -- Storing salt for password hashing
    password NVARCHAR(MAX),       -- Storing plain text password (temporary)
    created_at DATETIME DEFAULT GETDATE(),
    last_login DATETIME NULL,
    profile_image NVARCHAR(MAX),
    bio NVARCHAR(MAX),
    is_verified BIT DEFAULT 0,
    subscription_status NVARCHAR(MAX)
);
GO


-- Videos Table
CREATE TABLE Videos (
    IDVideo INT PRIMARY KEY IDENTITY(1,1),
    user_id INT,
    title NVARCHAR(MAX),
    description NVARCHAR(MAX),
    upload_date DATETIME,
    thumbnail_url NVARCHAR(MAX),
    video_url NVARCHAR(MAX),
    duration INT NULL,
    categories NVARCHAR(MAX),
    privacy_setting NVARCHAR(MAX),
    total_likes INT DEFAULT 0,
    total_views INT DEFAULT 0,
    total_subscribers INT DEFAULT 0,
    FOREIGN KEY (user_id) REFERENCES Users(IDUser)
);
GO

-- Likes/Dislikes Table
CREATE TABLE LikesDislikes (
    like_id INT PRIMARY KEY IDENTITY(1,1),
    user_id INT,
    video_id INT,
    like_status INT,
    UNIQUE (user_id, video_id, like_status),
    FOREIGN KEY (user_id) REFERENCES Users(IDUser),
    FOREIGN KEY (video_id) REFERENCES Videos(IDVideo)
);
GO

-- Views Table
CREATE TABLE Views (
    view_id INT PRIMARY KEY IDENTITY(1,1),
    user_id INT,
    video_id INT,
    view_date DATETIME,
    UNIQUE (user_id, video_id, view_date),
    FOREIGN KEY (user_id) REFERENCES Users(IDUser),
    FOREIGN KEY (video_id) REFERENCES Videos(IDVideo)
);
GO

-- Subscriptions Table
CREATE TABLE Subscriptions (
    subscription_id INT PRIMARY KEY IDENTITY(1,1),
    subscriber_id INT,
    channel_id INT,
    UNIQUE (subscriber_id, channel_id),
    FOREIGN KEY (subscriber_id) REFERENCES Users(IDUser),
    FOREIGN KEY (channel_id) REFERENCES Users(IDUser)
);
GO

-- Comments Table
CREATE TABLE Comments (
    comment_id INT PRIMARY KEY IDENTITY(1,1),
    user_id INT,
    video_id INT,
    comment_text NVARCHAR(MAX),
    comment_date DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (user_id) REFERENCES Users(IDUser),
    FOREIGN KEY (video_id) REFERENCES Videos(IDVideo)
);
GO

-- Tags Table
CREATE TABLE Tags (
    tag_id INT PRIMARY KEY IDENTITY(1,1),
    tag_name NVARCHAR(MAX) UNIQUE
);
GO

-- Genres Table
CREATE TABLE Genres (
    genre_id INT PRIMARY KEY IDENTITY(1,1),
    genre_name NVARCHAR(MAX) UNIQUE
);
GO

-- VideoTags Table
CREATE TABLE VideoTags (
    video_id INT,
    tag_id INT,
    PRIMARY KEY (video_id, tag_id),
    FOREIGN KEY (video_id) REFERENCES Videos(IDVideo),
    FOREIGN KEY (tag_id) REFERENCES Tags(tag_id)
);
GO

-- VideoGenres Table
CREATE TABLE VideoGenres (
    video_id INT,
    genre_id INT,
    PRIMARY KEY (video_id, genre_id),
    FOREIGN KEY (video_id) REFERENCES Videos(IDVideo),
    FOREIGN KEY (genre_id) REFERENCES Genres(genre_id)
);
GO

-- Roles Table
CREATE TABLE Roles (
    role_id INT PRIMARY KEY IDENTITY(1,1),
    role_name NVARCHAR(MAX) UNIQUE
);

--Procedure
-- Create User Procedure
go
CREATE PROCEDURE CreateUser
    @first_name NVARCHAR(MAX),
    @last_name NVARCHAR(MAX),
    @username NVARCHAR(225),
    @email NVARCHAR(255),
    @password_hash NVARCHAR(MAX),
    @password_salt NVARCHAR(MAX),
    @profile_image NVARCHAR(MAX),
    @bio NVARCHAR(MAX),
    @is_verified BIT = 0,
    @subscription_status NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO Users (first_name, last_name, username, email, password_hash, password_salt, profile_image, bio, is_verified, subscription_status)
    VALUES (@first_name, @last_name, @username, @email, @password_hash, @password_salt, @profile_image, @bio, @is_verified, @subscription_status);
END;
go
-- Read User Procedure
CREATE PROCEDURE ReadUser
    @userID INT
AS
BEGIN
    SELECT * FROM Users WHERE IDUser = @userID;
END;
go
-- Update User Procedure
CREATE PROCEDURE UpdateUser
    @userID INT,
    @first_name NVARCHAR(MAX),
    @last_name NVARCHAR(MAX),
    @profile_image NVARCHAR(MAX),
    @bio NVARCHAR(MAX)
AS
BEGIN
    UPDATE Users
    SET first_name = @first_name,
        last_name = @last_name,
        profile_image = @profile_image,
        bio = @bio
    WHERE IDUser = @userID;
END;
go
-- Delete User Procedure
CREATE PROCEDURE DeleteUser
    @userID INT
AS
BEGIN
    DELETE FROM Users WHERE IDUser = @userID;
END;
go
-- Create Video Procedure
CREATE PROCEDURE CreateVideo
    @user_id INT,
    @title NVARCHAR(MAX),
    @description NVARCHAR(MAX),
    @upload_date DATETIME,
    @thumbnail_url NVARCHAR(MAX),
    @video_url NVARCHAR(MAX),
    @categories NVARCHAR(MAX),
    @privacy_setting NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO Videos (user_id, title, description, upload_date, thumbnail_url, video_url, categories, privacy_setting)
    VALUES (@user_id, @title, @description, @upload_date, @thumbnail_url, @video_url, @categories, @privacy_setting);
END;
go
-- Read Video Procedure
CREATE PROCEDURE ReadVideo
    @videoID INT
AS
BEGIN
    SELECT * FROM Videos WHERE IDVideo = @videoID;
END;
go
-- Update Video Procedure
CREATE PROCEDURE UpdateVideo
    @videoID INT,
    @title NVARCHAR(MAX),
    @description NVARCHAR(MAX),
    @thumbnail_url NVARCHAR(MAX),
    @video_url NVARCHAR(MAX)
AS
BEGIN
    UPDATE Videos
    SET title = @title,
        description = @description,
        thumbnail_url = @thumbnail_url,
        video_url = @video_url
    WHERE IDVideo = @videoID;
END;
go
-- Delete Video Procedure
CREATE PROCEDURE DeleteVideo
    @videoID INT
AS
BEGIN
    DELETE FROM Videos WHERE IDVideo = @videoID;
END;
go
-- Create Like/Dislike Procedure
CREATE PROCEDURE CreateLikeDislike
    @userID INT,
    @videoID INT,
    @like_status INT
AS
BEGIN
    INSERT INTO LikesDislikes (user_id, video_id, like_status)
    VALUES (@userID, @videoID, @like_status);
END;
go
-- Read Like/Dislike Procedure
CREATE PROCEDURE ReadLikeDislike
    @likeID INT
AS
BEGIN
    SELECT * FROM LikesDislikes WHERE like_id = @likeID;
END;
go
-- Create Subscription Procedure
CREATE PROCEDURE CreateSubscription
    @subscriberID INT,
    @channelID INT
AS
BEGIN
    INSERT INTO Subscriptions (subscriber_id, channel_id)
    VALUES (@subscriberID, @channelID);
END;
go
-- Read Subscription Procedure
CREATE PROCEDURE ReadSubscription
    @subscriptionID INT
AS
BEGIN
    SELECT * FROM Subscriptions WHERE subscription_id = @subscriptionID;
END;
go
-- Delete Subscription Procedure
CREATE PROCEDURE DeleteSubscription
    @subscriptionID INT
AS
BEGIN
    DELETE FROM Subscriptions WHERE subscription_id = @subscriptionID;
END;
go
-- Create Comment Procedure
CREATE PROCEDURE CreateComment
    @userID INT,
    @videoID INT,
    @commentText NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO Comments (user_id, video_id, comment_text)
    VALUES (@userID, @videoID, @commentText);
END;
go
-- Read Comment Procedure
CREATE PROCEDURE ReadComment
    @commentID INT
AS
BEGIN
    SELECT * FROM Comments WHERE comment_id = @commentID;
END;
go
-- Update Comment Procedure
CREATE PROCEDURE UpdateComment
    @commentID INT,
    @commentText NVARCHAR(MAX)
AS
BEGIN
    UPDATE Comments
    SET comment_text = @commentText
    WHERE comment_id = @commentID;
END;
go
-- Delete Comment Procedure
CREATE PROCEDURE DeleteComment
    @commentID INT
AS
BEGIN
    DELETE FROM Comments WHERE comment_id = @commentID;
END;
go
-- Create Tag Procedure
CREATE PROCEDURE CreateTag
    @tagName NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO Tags (tag_name)
    VALUES (@tagName);
END;
go
-- Read Tag Procedure
CREATE PROCEDURE ReadTag
    @tagID INT
AS
BEGIN
    SELECT * FROM Tags WHERE tag_id = @tagID;
END;
go

-- Create Genre Procedure
CREATE PROCEDURE CreateGenre
    @genreName NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO Genres (genre_name)
    VALUES (@genreName);
END;
go
-- Read Genre Procedure
CREATE PROCEDURE ReadGenre
    @genreID INT
AS
BEGIN
    SELECT * FROM Genres WHERE genre_id = @genreID;
END;
go

