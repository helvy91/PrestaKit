-- Enable webservice
INSERT INTO ps_configuration (name, value, date_add, date_upd)
VALUES ('PS_WEBSERVICE', '1', NOW(), NOW());

-- Create the test API key
INSERT INTO ps_webservice_account (`key`, description, active)
VALUES ('TESTKEY000000000000000000000000A', 'test', 1);

-- Grant all methods on all tested resources
INSERT INTO ps_webservice_permission (id_webservice_account, resource, method)
SELECT a.id_webservice_account, r.resource, m.method
FROM ps_webservice_account a
CROSS JOIN (SELECT 'products' AS resource UNION SELECT 'categories' UNION SELECT 'images'
            UNION SELECT 'attachments' UNION SELECT 'stock_availables' UNION SELECT 'customers'
            UNION SELECT 'manufacturers' UNION SELECT 'suppliers' UNION SELECT 'combinations'
            UNION SELECT 'addresses') r
CROSS JOIN (SELECT 'GET' AS method UNION SELECT 'POST' UNION SELECT 'PUT'
            UNION SELECT 'DELETE' UNION SELECT 'HEAD') m
WHERE a.`key` = 'TESTKEY000000000000000000000000A';

-- Associate the account with shop 1
INSERT INTO ps_webservice_account_shop (id_webservice_account, id_shop)
SELECT id_webservice_account, 1 FROM ps_webservice_account
WHERE `key` = 'TESTKEY000000000000000000000000A';