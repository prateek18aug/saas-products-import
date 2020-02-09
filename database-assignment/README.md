# SQL Test Assignment

Attached is a mysqldump of a database to be used during the test.

Below are the questions for this test. Please enter a full, complete, working SQL statement under each question. We do not want the answer to the question. We want the SQL command to derive the answer. We will copy/paste these commands to test the validity of the answer.

**Example:**

_Q. Select all users_

- Please return at least first_name and last_name

- SELECT first_name, last_name FROM users;


------

**— Test Starts Here —**

1. Select users whose id is either 3,2 or 4
- Please return at least: all user fields

- SELECT id, first_name, last_name, email, status, created
FROM users
WHERE ID IN(2,3,4)

2. Count how many basic and premium listings each active user has
- Please return at least: first_name, last_name, basic, premium

- SELECT uout.first_name, uout.last_name, (SELECT Count(*) FROM
test.listings l
WHERE l.status = 2 AND l.user_id = uout.id) AS BASIC, 
(SELECT Count(*) FROM
test.listings l
WHERE l.status = 3 AND l.user_id =uout.id) AS PREMIUM 
FROM test.listings lout
INNER JOIN test.users uout
ON lout.user_id = uout.id 
WHERE uout.status = 2
group by uout.id


3. Show the same count as before but only if they have at least ONE premium listing
- Please return at least: first_name, last_name, basic, premium

- SELECT uout.first_name, uout.last_name, (SELECT Count(*) FROM
test.listings l
WHERE l.status = 2 AND l.user_id = uout.id) AS BASIC, 
(SELECT Count(*) FROM
test.listings l
WHERE l.status = 3 AND l.user_id =uout.id) AS PREMIUM 
FROM test.listings lout
INNER JOIN test.users uout
ON lout.user_id = uout.id 
WHERE uout.status = 2
group by uout.id
having PREMIUM != 0

4. How much revenue has each active vendor made in 2013
- Please return at least: first_name, last_name, currency, revenue

- SELECT u.first_name, u.last_name,c.currency, sum(c.price) as revenue FROM
test.listings l
INNER JOIN test.users u
ON u.id = l.user_id
INNER JOIN test.clicks c
ON c.listing_id = l.id
WHERE u.status = 2 AND c.created > '2012-12-31 23:59:59' AND c.created < '2014-01-01 00:00:00'
GROUP BY u.id, c.currency

5. Insert a new click for listing id 3, at $4.00
- Find out the id of this new click. Please return at least: id

- INSERT INTO test.clicks(listing_id, price, currency, created) VALUES (3,4.00,'USD',NOW())
SELECT LAST_INSERT_ID();

6. Show listings that have not received a click in 2013
- Please return at least: listing_name

- SELECT DISTINCT(l.name)
FROM test.listings l
INNER JOIN test.clicks c1
ON c1.listing_id = l.id
LEFT JOIN (
SELECT DISTINCT(c.listing_id) AS listing_id
FROM test.clicks c
WHERE c.created > '2012-12-31 23:59:59' AND c.created < '2014-01-01 00:00:00') c2
ON c1.listing_id = c2.listing_id
WHERE c2.listing_id IS NULL

7. For each year show number of listings clicked and number of vendors who owned these listings
- Please return at least: date, total_listings_clicked, total_vendors_affected

- SELECT year(c.created) AS Date, COUNT(c.id) AS total_listings_clicked, COUNT(distinct(u.id)) AS total_vendors_affected
FROM test.users u
INNER JOIN test.listings l
ON u.id = l.user_id
INNER JOIN test.clicks c
ON c.listing_id = l.id
group by year(c.created)

8. Return a comma separated string of listing names for all active vendors
- Please return at least: first_name, last_name, listing_names

- SELECT u.first_name, u.last_name, group_concat(distinct l.name) AS listing_names
FROM test.users u
INNER JOIN test.listings l
ON u.id = l.user_id
WHERE u.status = 2
Group by u.id
