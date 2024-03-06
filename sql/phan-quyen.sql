﻿USE HotelManagementSystem;

CREATE ROLE Staff;

--------Gán các quyền trên các bảng cho role Staff---------
GRANT SELECT, REFERENCES ON ACCOUNT TO Staff 
GRANT SELECT, INSERT, DELETE, UPDATE, REFERENCES ON BILL TO Staff
GRANT SELECT, INSERT, DELETE, UPDATE, REFERENCES ON BOOKING_RECORD TO Staff
GRANT SELECT, INSERT, DELETE, UPDATE, REFERENCES ON CUSTOMER TO Staff
GRANT SELECT, INSERT, DELETE, UPDATE, REFERENCES ON CUSTOMER_OF_BOOKING_RECORD TO Staff
GRANT SELECT, INSERT, DELETE, UPDATE, REFERENCES ON PHONE_NUMBER_OF_CUSTOMER TO Staff
GRANT SELECT, UPDATE, REFERENCES ON ROOM TO Staff
GRANT SELECT, REFERENCES ON ROOM_TYPE TO Staff
GRANT SELECT, UPDATE, REFERENCES ON SERVICE_ROOM TO Staff
GRANT SELECT, INSERT, DELETE, UPDATE, REFERENCES ON SERVICE_USAGE_INFOR TO Staff

-- Gán quyền thực thi trên các procedure, function cho role Staff
GRANT EXECUTE TO Staff;
GRANT SELECT TO Staff;

DENY EXECUTE ON proc_add_room to Staff;
DENY EXECUTE ON proc_add_room_type to Staff;
DENY EXECUTE ON proc_delete_room to Staff;
DENY EXECUTE ON proc_delete_room_type to Staff;
DENY EXECUTE ON proc_deleteAccount to Staff;
DENY EXECUTE ON proc_deleteEmployee to Staff;
DENY EXECUTE ON proc_deleteServiceRoom to Staff;
DENY EXECUTE ON proc_insertServiceRoom to Staff;
DENY EXECUTE ON proc_update_room_type to Staff;

GRANT  EXECUTE ON dbo.f_Calculate_Total_Revenue to Staff;
GRANT SELECT ON dbo.f_Calculate_Revenue to Staff;