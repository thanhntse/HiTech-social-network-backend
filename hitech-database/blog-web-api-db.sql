create database blog
use blog

-- Create Tag table
CREATE TABLE dbo.tag (
    tag_id INT IDENTITY(1,1) PRIMARY KEY,
    tag_name NVARCHAR(MAX) NOT NULL
);

-- Create Blog table
CREATE TABLE dbo.blog (
    blog_id INT IDENTITY(1,1) PRIMARY KEY,
    title NVARCHAR(MAX) NOT NULL,
    content NVARCHAR(MAX) NOT NULL,
    created_at DATETIME NOT NULL,
    updated_at DATETIME,
    published_at DATETIME,
    [like] INT NOT NULL DEFAULT 0,
    comments_count INT NOT NULL DEFAULT 0,
    is_deleted BIT NOT NULL DEFAULT 0,
    author_id INT NOT NULL
);

-- Create Comment table
CREATE TABLE dbo.comment (
    comment_id INT IDENTITY(1,1) PRIMARY KEY,
    content NVARCHAR(MAX) NOT NULL,
    created_at DATETIME NOT NULL,
    updated_at DATETIME,
    author_id INT NOT NULL,
    blog_id INT NOT NULL,
    FOREIGN KEY (blog_id) REFERENCES dbo.blog(blog_id)
);


-- Create BlogTag table
CREATE TABLE dbo.blog_tag (
    blog_id INT NOT NULL,
    tag_id INT NOT NULL,
    PRIMARY KEY (blog_id, tag_id),
    FOREIGN KEY (blog_id) REFERENCES dbo.blog(blog_id),
    FOREIGN KEY (tag_id) REFERENCES dbo.tag(tag_id)
);

