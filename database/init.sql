DROP TABLE IF EXISTS quotes;
DROP TABLE IF EXISTS users;
DROP TABLE IF EXISTS likes;
DROP TABLE IF EXISTS comments;
DROP TABLE IF EXISTS bookmarks;

CREATE TABLE IF NOT EXISTS users (
    id SERIAL PRIMARY KEY,
    userName VARCHAR(50) NOT NULL,
    createdAt TIMESTAMPTZ NOT NULL,
    updatedAt TIMESTAMPTZ NOT NULL
);

CREATE TABLE IF NOT EXISTS quotes (
    id SERIAL PRIMARY KEY,
    userId INTEGER REFERENCES users(id),
    quote VARCHAR NOT NULL,
    createdAt TIMESTAMPTZ NOT NULL,
    updatedAt TIMESTAMPTZ NOT NULL
);

CREATE TABLE IF NOT EXISTS likes (
    userId INTEGER REFERENCES users(id),
    quoteId INTEGER REFERENCES quotes(id),
    PRIMARY KEY(quoteId, userId)
);

CREATE TABLE IF NOT EXISTS comments (
    id SERIAL PRIMARY KEY,
    userId INTEGER REFERENCES users(id),
    quoteId INTEGER REFERENCES quotes(id),
    comment VARCHAR NOT NULL,
    createdAt TIMESTAMPTZ NOT NULL
);

CREATE TABLE IF NOT EXISTS bookmarks (
    id SERIAL PRIMARY KEY,
    userId INTEGER REFERENCES users(id),
    quoteId INTEGER REFERENCES quotes(id)
);

-- Create indicies
CREATE INDEX ON users(lower(username));

CREATE INDEX ON quotes(userId);

CREATE INDEX ON comments(quoteId);

CREATE INDEX ON bookmarks(userId, quoteId);

-- Users
INSERT INTO users (userName, createdAt, updatedAt) VALUES ('Randy Savage', NOW(), NOW());
INSERT INTO users (userName, createdAt, updatedAt) VALUES ('Stone Cold', NOW(), NOW());
INSERT INTO users (userName, createdAt, updatedAt) VALUES ('Agent Smith', NOW(), NOW());

-- Quotes
INSERT INTO quotes(userId, quote, createdAt, updatedAt) VALUES (2, 'Thats the bottom line cause Stone Cold said so', NOW(), NOW());
INSERT INTO quotes(userId, quote, createdAt, updatedAt) VALUES (2, 'Austin 3:16 said I just whopped your a**', NOW(), NOW());
INSERT INTO quotes(userId, quote, createdAt, updatedAt) VALUES (3, 'Mr Anderson!', NOW(), NOW());
INSERT INTO quotes(userId, quote, createdAt, updatedAt) VALUES (1, 'Im the tower of power. Too sweet to be sour', NOW(), NOW());
INSERT INTO quotes(userId, quote, createdAt, updatedAt) VALUES (3, 'We are all free because of you Mr Anderson', NOW(), NOW());

-- Comments
INSERT INTO comments(userId, quoteId, comment, createdAt) VALUES (1, 2, 'Let me tell you now a man of my position can afford to look ridiculous at any time', NOW());
INSERT INTO comments(userId, quoteId, comment, createdAt) VALUES (2, 2, 'What?!', NOW());

-- Bookmarks
INSERT INTO bookmarks(userId, quoteId) VALUES (3, 4);