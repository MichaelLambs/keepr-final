-- CREATE TABLE users(
--   id VARCHAR(255) NOT NULL,
--   name VARCHAR(255) NOT NULL,
--   email VARCHAR(255) NOT NULL,
--   password VARCHAR(255) NOT NULL,
--   PRIMARY KEY (id),
--   UNIQUE KEY email (email)
-- );

-- CREATE TABLE keeps(
--   id VARCHAR(255) NOT NULL,
--   name VARCHAR(255) NOT NULL,
--   description VARCHAR(255),
--   mediaUrl VARCHAR(255) NOT NULL,
--   userId VARCHAR(255) NOT NULL,
--   addedToBoard int,
--   keepViews int,
--   keepPublic int,
--   INDEX userId (userId),
--   FOREIGN KEY (userId)
--       REFERENCES users(id)
--       ON DELETE CASCADE,
--   PRIMARY KEY (id)
-- );


-- CREATE TABLE boards(
--   id VARCHAR(255) NOT NULL,
--   name VARCHAR(255) NOT NULL,
--   userId VARCHAR(255) NOT NULL,
--   INDEX userId (userId),
--   FOREIGN KEY (userId)
--       REFERENCES users(id)
--       ON DELETE CASCADE,
--   PRIMARY KEY (id)
-- );


-- CREATE TABLE keepsreport(
--     id VARCHAR(255) NOT NULL,
--     boardId VARCHAR(255) NOT NULL,
--     keepId VARCHAR(255) NOT NULL,
--     userId VARCHAR(255) NOT NULL,

--     PRIMARY KEY (id),
--     INDEX (boardId, keepId),
--     INDEX (userId),

--     FOREIGN KEY (userId)
--         REFERENCES users(id),

--     FOREIGN KEY (boardId)
--         REFERENCES boards(id),

--     FOREIGN KEY (keepId)
--         REFERENCES keeps(id)
-- );


-- by looking at valukeeps find me all the keeps that are at the user id. then find be 


-- find me all the keeps where valut id is 2 then bring in the keeps where keeps math//


-- SELECT * FROM keeps
-- WHERE (userId = "b8eaf35a-b673-47ad-a219-796d8f23f850");


-- SELECT * FROM keepsreport

-- What links keeps and boards together?
-- How do you make it so a user can only delete keeps he creates
-- Things to do tomorrow.
-- GET BACKEND WORKING #1 GOAL FUCK THE FRONTEND
-- Get keeps by board, add keep to board, updata board, delete board, 
-- DELETE ALL TABLES AND START FRESH


-- SELECT
--     k.name keepName,
--     k.description,
--     k.mediaUrl,
--     k.addedToBoard,
--     k.keepViews,
--     b.name boardName,
--     u.name username

-- FROM keepsreport kr
-- JOIN keeps k ON k.id = kr.keepId
-- JOIN boards b ON b.id = kr.boardId
-- JOIN users u ON u.id = kr.userId
-- WHERE boardId = "50470322-687b-437c-ba46-9596b4d366c0";



-- DELETE FROM keepsreport
-- WHERE keepId IN (
--     SELECT keepId FROM keeps
--     WHERE keepId = "42382fdb-e648-454e-a514-2c5b6ffa6819"
-- );

-- DELETE FROM keepsreport
-- WHERE keepId = "60930834-82ff-4128-86a5-2a747c1e796d" AND boardId = "50470322-687b-437c-ba46-9596b4d366c0";
