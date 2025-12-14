SELECT 
    o.order_id,
    c.customer_name,
    o.order_total
FROM 
    Orders o
INNER JOIN 
    Customers c ON o.customer_id = c.customer_id;