--
-- PostgreSQL database dump
--

\restrict BIWFxUr3lKfIg6elVkmVXEbruZ6zaxGySWUeVZzZMBjbhBPt0VvCS9XfaSymiaL

-- Dumped from database version 18.6
-- Dumped by pg_dump version 18.6

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Data for Name: tbl_BookingCategory; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."tbl_BookingCategory" ("CatID", "Category", "Created_By", "Created_Timestamp") VALUES (1, 'Donar', 'Admin', '2026-05-18 18:10:48.1');
INSERT INTO public."tbl_BookingCategory" ("CatID", "Category", "Created_By", "Created_Timestamp") VALUES (2, 'Padmsali', 'Admin', '2026-05-18 18:10:48.1');
INSERT INTO public."tbl_BookingCategory" ("CatID", "Category", "Created_By", "Created_Timestamp") VALUES (3, 'Devastanam', 'Admin', '2026-05-18 18:10:48.1');
INSERT INTO public."tbl_BookingCategory" ("CatID", "Category", "Created_By", "Created_Timestamp") VALUES (4, 'Others', NULL, '2026-05-18 18:10:48.1');


--
-- Data for Name: tbl_BookingStatus; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."tbl_BookingStatus" ("BookingStatusID", "Status_Code", "BookingStatusDesc", "Notes", "Created_By", "Created_Timestamp") VALUES (1, 'CO', 'CheckOut', NULL, 'admin', '2026-05-12 17:56:25.587');
INSERT INTO public."tbl_BookingStatus" ("BookingStatusID", "Status_Code", "BookingStatusDesc", "Notes", "Created_By", "Created_Timestamp") VALUES (2, 'CI', 'CheckIn', NULL, 'admin', '2026-05-12 17:56:33.82');
INSERT INTO public."tbl_BookingStatus" ("BookingStatusID", "Status_Code", "BookingStatusDesc", "Notes", "Created_By", "Created_Timestamp") VALUES (3, 'CN', 'Cancelled', NULL, 'admin', '2026-05-12 17:56:49.613');
INSERT INTO public."tbl_BookingStatus" ("BookingStatusID", "Status_Code", "BookingStatusDesc", "Notes", "Created_By", "Created_Timestamp") VALUES (4, 'BK', 'Booked', NULL, 'admin', '2026-05-12 17:56:55.853');
INSERT INTO public."tbl_BookingStatus" ("BookingStatusID", "Status_Code", "BookingStatusDesc", "Notes", "Created_By", "Created_Timestamp") VALUES (5, 'RE', 'Reserved', NULL, 'admin', '2026-05-12 17:57:14.4');
INSERT INTO public."tbl_BookingStatus" ("BookingStatusID", "Status_Code", "BookingStatusDesc", "Notes", "Created_By", "Created_Timestamp") VALUES (6, 'SH', 'Shifted', NULL, 'admin', '2026-05-15 18:01:13.733');


--
-- Data for Name: tbl_Country; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."tbl_Country" ("CountryID", "CountryName", "Created_By", "Created_Timestamp") VALUES (1, 'India', 'Admin', '2026-05-02 09:03:28.81');
INSERT INTO public."tbl_Country" ("CountryID", "CountryName", "Created_By", "Created_Timestamp") VALUES (2, 'USA', 'Admin', '2026-05-02 09:03:33.86');
INSERT INTO public."tbl_Country" ("CountryID", "CountryName", "Created_By", "Created_Timestamp") VALUES (3, 'Other', 'Admin', '2026-05-02 09:03:43.957');


--
-- Data for Name: tbl_Districts; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (1, 1, 1, 'Alluri Sitharama Raju', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (2, 1, 1, 'Anakapalli', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (3, 1, 1, 'Ananthapuramu', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (4, 1, 1, 'Annamayya', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (5, 1, 1, 'Bapatla', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (6, 1, 1, 'Chittoor', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (7, 1, 1, 'Dr. B.R. Ambedkar Konaseema', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (8, 1, 1, 'East Godavari', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (9, 1, 1, 'Eluru', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (10, 1, 1, 'Guntur', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (11, 1, 1, 'Kakinada', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (12, 1, 1, 'Krishna', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (13, 1, 1, 'Kurnool', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (14, 1, 1, 'Nandyal', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (15, 1, 1, 'Nellore', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (16, 1, 1, 'NTR', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (17, 1, 1, 'Palnadu', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (18, 1, 1, 'Parvathipuram Manyam', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (19, 1, 1, 'Prakasam', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (20, 1, 1, 'Srikakulam', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (21, 1, 1, 'Sri Potti Sriramulu Nellore', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (22, 1, 1, 'Sri Sathya Sai', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (23, 1, 1, 'Tirupati', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (24, 1, 1, 'Visakhapatnam', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (25, 1, 1, 'Vizianagaram', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (26, 1, 1, 'West Godavari', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (27, 1, 1, 'YSR Kadapa', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (28, 24, 1, 'Adilabad', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (29, 24, 1, 'Bhadradri Kothagudem', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (30, 24, 1, 'Hanumakonda', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (31, 24, 1, 'Hyderabad', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (32, 24, 1, 'Jagtial', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (33, 24, 1, 'Jangaon', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (34, 24, 1, 'Jayashankar Bhupalpally', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (35, 24, 1, 'Jogulamba Gadwal', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (36, 24, 1, 'Kamareddy', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (37, 24, 1, 'Karimnagar', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (38, 24, 1, 'Khammam', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (39, 24, 1, 'Komaram Bheem Asifabad', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (40, 24, 1, 'Mahabubabad', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (41, 24, 1, 'Mahabubnagar', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (42, 24, 1, 'Mancherial', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (43, 24, 1, 'Medak', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (44, 24, 1, 'Medchal–Malkajgiri', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (45, 24, 1, 'Mulugu', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (46, 24, 1, 'Nagarkurnool', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (47, 24, 1, 'Nalgonda', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (48, 24, 1, 'Narayanpet', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (49, 24, 1, 'Nirmal', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (50, 24, 1, 'Nizamabad', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (51, 24, 1, 'Peddapalli', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (52, 24, 1, 'Rajanna Sircilla', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (53, 24, 1, 'Rangareddy', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (54, 24, 1, 'Sangareddy', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (55, 24, 1, 'Siddipet', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (56, 24, 1, 'Suryapet', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (57, 24, 1, 'Vikarabad', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (58, 24, 1, 'Wanaparthy', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (59, 24, 1, 'Warangal', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (60, 24, 1, 'Yadadri Bhuvanagiri', 'system', '2026-05-17 16:53:35.487');
INSERT INTO public."tbl_Districts" ("DistrictID", "StateID", "CountryID", "DistrictName", "Created_By", "Created_Timestamp") VALUES (61, 0, 0, 'Other', 'system', '2026-05-17 16:53:35.487');


--
-- Data for Name: tbl_DonarType_Master; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."tbl_DonarType_Master" ("DTID", "DONARTYPE", "DAYS", "CODE", "AMOUNT", "Created_By", "Created_Timestamp") VALUES (4, 'ONE DAY ANNADANAM', 0, 'OD', 1116, 'Admin', '2026-05-18 18:11:06.023');
INSERT INTO public."tbl_DonarType_Master" ("DTID", "DONARTYPE", "DAYS", "CODE", "AMOUNT", "Created_By", "Created_Timestamp") VALUES (5, 'MAHARAJA POSHAKULU', 10, 'MP', 100116, 'Admin', '2026-05-18 18:11:06.023');
INSERT INTO public."tbl_DonarType_Master" ("DTID", "DONARTYPE", "DAYS", "CODE", "AMOUNT", "Created_By", "Created_Timestamp") VALUES (6, 'RAJA POSHAKULU', 5, 'RP', 50116, 'Admin', '2026-05-18 18:11:06.023');
INSERT INTO public."tbl_DonarType_Master" ("DTID", "DONARTYPE", "DAYS", "CODE", "AMOUNT", "Created_By", "Created_Timestamp") VALUES (7, 'POSHAKULU', 2, 'PO', 25116, 'Admin', '2026-05-18 18:11:06.023');
INSERT INTO public."tbl_DonarType_Master" ("DTID", "DONARTYPE", "DAYS", "CODE", "AMOUNT", "Created_By", "Created_Timestamp") VALUES (8, 'DATHALU', 0, 'DA', 11116, 'Admin', '2026-05-18 18:11:06.023');
INSERT INTO public."tbl_DonarType_Master" ("DTID", "DONARTYPE", "DAYS", "CODE", "AMOUNT", "Created_By", "Created_Timestamp") VALUES (9, 'OTHER DONATION', 0, 'OT', 1, 'Admin', '2026-05-18 18:11:06.023');
INSERT INTO public."tbl_DonarType_Master" ("DTID", "DONARTYPE", "DAYS", "CODE", "AMOUNT", "Created_By", "Created_Timestamp") VALUES (10, 'AKSHAYA MAHARAJA POSHAKULU', 30, 'AK', 500116, 'Admin', '2026-05-25 18:18:48.627');
INSERT INTO public."tbl_DonarType_Master" ("DTID", "DONARTYPE", "DAYS", "CODE", "AMOUNT", "Created_By", "Created_Timestamp") VALUES (11, 'ANNADANAM DONATION', 0, 'AD', 2516, 'Admin', '2026-05-25 18:19:53.907');
INSERT INTO public."tbl_DonarType_Master" ("DTID", "DONARTYPE", "DAYS", "CODE", "AMOUNT", "Created_By", "Created_Timestamp") VALUES (12, 'ONEDAY SWEET DATHA', 0, 'OS', 1516, 'Admin', '2026-05-25 18:20:11.537');
INSERT INTO public."tbl_DonarType_Master" ("DTID", "DONARTYPE", "DAYS", "CODE", "AMOUNT", "Created_By", "Created_Timestamp") VALUES (61, 'AutoTest Type 22303450', 2, 'ZZ', 111, 'uitest', '2026-07-09 17:00:39.02');


--
-- Data for Name: tbl_Floor_Master; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."tbl_Floor_Master" ("Floor_ID", "Floor_Description", "Active", "Created_By", "Created_Timestamp") VALUES (1, 'Ground Floor', 'Y', 'Admin', '2026-05-01 13:56:09.82');
INSERT INTO public."tbl_Floor_Master" ("Floor_ID", "Floor_Description", "Active", "Created_By", "Created_Timestamp") VALUES (2, 'First Floor', 'Y', 'Admin', '2026-05-01 13:56:09.82');
INSERT INTO public."tbl_Floor_Master" ("Floor_ID", "Floor_Description", "Active", "Created_By", "Created_Timestamp") VALUES (3, 'Second Floor', 'Y', 'Admin', '2026-05-01 13:56:09.82');
INSERT INTO public."tbl_Floor_Master" ("Floor_ID", "Floor_Description", "Active", "Created_By", "Created_Timestamp") VALUES (4, 'General', 'Y', 'Admin', '2026-05-01 13:56:09.82');


--
-- Data for Name: tbl_Occassion_Master; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."tbl_Occassion_Master" ("OccassionID", "OccassionName", "Created_By", "Created_Timestamp") VALUES (1, 'Marriage Day Of', 'Admin', '2026-05-18 18:11:22.727');
INSERT INTO public."tbl_Occassion_Master" ("OccassionID", "OccassionName", "Created_By", "Created_Timestamp") VALUES (2, 'Birthday of ', 'Admin', '2026-05-18 18:11:22.727');
INSERT INTO public."tbl_Occassion_Master" ("OccassionID", "OccassionName", "Created_By", "Created_Timestamp") VALUES (3, 'In Memory of ', 'Admin', '2026-05-18 18:11:22.727');
INSERT INTO public."tbl_Occassion_Master" ("OccassionID", "OccassionName", "Created_By", "Created_Timestamp") VALUES (4, 'Opening Cermony', 'Admin', '2026-05-18 18:11:22.727');
INSERT INTO public."tbl_Occassion_Master" ("OccassionID", "OccassionName", "Created_By", "Created_Timestamp") VALUES (5, 'Job Joining of ', 'Admin', '2026-05-18 18:11:22.727');
INSERT INTO public."tbl_Occassion_Master" ("OccassionID", "OccassionName", "Created_By", "Created_Timestamp") VALUES (6, 'Pournami roju', 'Admin', '2026-05-18 18:11:22.727');
INSERT INTO public."tbl_Occassion_Master" ("OccassionID", "OccassionName", "Created_By", "Created_Timestamp") VALUES (7, 'Others', 'Admin', '2026-05-18 18:11:22.727');


--
-- Data for Name: tbl_PaymentSubTypeMaster; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."tbl_PaymentSubTypeMaster" ("paymentSubTypeID", "PaymentSubType", "paymentTypeID", "Created_By", "Created_Timestamp") VALUES (1, 'PhonePe', 2, 'Admin', '2026-05-09 13:13:54.47');
INSERT INTO public."tbl_PaymentSubTypeMaster" ("paymentSubTypeID", "PaymentSubType", "paymentTypeID", "Created_By", "Created_Timestamp") VALUES (2, 'GPay', 2, 'Admin', '2026-05-09 13:14:13.34');
INSERT INTO public."tbl_PaymentSubTypeMaster" ("paymentSubTypeID", "PaymentSubType", "paymentTypeID", "Created_By", "Created_Timestamp") VALUES (3, 'Paytm', 2, 'Admin', '2026-05-09 13:14:24.637');
INSERT INTO public."tbl_PaymentSubTypeMaster" ("paymentSubTypeID", "PaymentSubType", "paymentTypeID", "Created_By", "Created_Timestamp") VALUES (4, 'Others', 2, 'Admin', '2026-05-09 13:14:34.557');
INSERT INTO public."tbl_PaymentSubTypeMaster" ("paymentSubTypeID", "PaymentSubType", "paymentTypeID", "Created_By", "Created_Timestamp") VALUES (5, 'Credit Card', 3, 'Admin', '2026-05-09 13:14:46.06');
INSERT INTO public."tbl_PaymentSubTypeMaster" ("paymentSubTypeID", "PaymentSubType", "paymentTypeID", "Created_By", "Created_Timestamp") VALUES (6, 'Debit Card', 3, 'Admin', '2026-05-09 13:14:58.717');


--
-- Data for Name: tbl_PaymentTypeMaster; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."tbl_PaymentTypeMaster" ("paymentTypeID", "PaymentType", "Created_By", "Created_Timestamp") VALUES (1, 'Cash', 'Admin', '2026-05-09 13:10:55.42');
INSERT INTO public."tbl_PaymentTypeMaster" ("paymentTypeID", "PaymentType", "Created_By", "Created_Timestamp") VALUES (2, 'Online', 'Admin', '2026-05-09 13:11:01.1');
INSERT INTO public."tbl_PaymentTypeMaster" ("paymentTypeID", "PaymentType", "Created_By", "Created_Timestamp") VALUES (3, 'Card', 'Admin', '2026-05-09 13:11:07.023');


--
-- Data for Name: tbl_Room; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (1, 1, 1, '101', 1200.00, 3, 6, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1700.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (2, 2, 1, '102', 900.00, 2, 4, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1400.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (3, 1, 1, '103', 1000.00, 2, 4, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1500.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (4, 2, 1, '104', 700.00, 2, 5, 1, 0, 'admin', '2026-05-03 06:39:33.63', 1200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (5, 1, 1, '105', 0.00, 2, 0, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 50.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (6, 1, 1, '106', 1300.00, 2, 6, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1800.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (7, 2, 1, '107', 1000.00, 2, 5, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1500.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (8, 2, 1, '108', 1000.00, 2, 5, 1, 0, 'admin', '2026-05-03 06:39:33.63', 1505.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (9, 2, 1, '109', 800.00, 2, 6, 1, 0, 'admin', '2026-05-03 06:39:33.63', 1300.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (10, 2, 1, '110', 700.00, 2, 5, 1, 0, 'admin', '2026-05-03 06:39:33.63', 1200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (11, 1, 1, '111', 1000.00, 2, 5, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1500.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (12, 1, 1, '112', 1000.00, 2, 5, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1500.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (13, 1, 1, '113', 1000.00, 2, 5, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1500.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (14, 1, 1, '114', 1300.00, 4, 8, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1800.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (15, 1, 1, '115', 1000.00, 2, 5, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1500.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (16, 2, 1, '116', 600.00, 2, 5, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (17, 1, 1, '117', 1300.00, 4, 10, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1800.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (18, 1, 1, '118', 1300.00, 4, 10, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1800.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (19, 2, 1, '119', 900.00, 4, 10, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1400.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (20, 2, 1, '120', 900.00, 4, 10, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1400.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (21, 2, 1, '121', 900.00, 4, 10, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1400.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (22, 2, 1, '122', 900.00, 4, 10, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1400.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (23, 1, 1, '123', 1000.00, 2, 4, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1500.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (24, 2, 1, '125', 500.00, 1, 2, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1000.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (25, 1, 1, '126', 1000.00, 2, 5, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1500.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (26, 2, 1, '128', 500.00, 2, 5, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1000.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (27, 1, 1, '134', 1200.00, 3, 8, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1700.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (28, 2, 2, '229', 500.00, 2, 4, 1, 0, 'admin', '2026-05-03 06:39:33.63', 1000.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (29, 2, 2, '230', 500.00, 2, 4, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1000.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (30, 2, 2, '231', 500.00, 2, 4, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1000.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (31, 2, 2, '232', 500.00, 2, 4, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1000.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (32, 2, 2, '233', 500.00, 2, 4, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1000.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (33, 2, 2, '234', 500.00, 2, 4, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1000.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (34, 2, 2, '235', 500.00, 2, 4, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1000.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (35, 2, 2, '236', 500.00, 2, 4, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1000.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (36, 2, 2, '237', 600.00, 2, 5, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (37, 2, 2, '238', 700.00, 2, 5, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (38, 2, 2, '239', 600.00, 2, 5, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (39, 1, 2, '240', 1300.00, 4, 8, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1800.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (40, 2, 2, '241', 700.00, 2, 5, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (41, 2, 2, '242', 600.00, 2, 5, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (42, 2, 2, '243', 600.00, 2, 5, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (43, 2, 2, '244', 600.00, 2, 5, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (44, 2, 2, '245', 600.00, 2, 5, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (45, 2, 2, '246', 600.00, 2, 5, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (46, 1, 2, '247', 1200.00, 3, 8, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1700.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (47, 1, 2, '248', 1200.00, 3, 8, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1700.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (48, 1, 2, '249', 1200.00, 3, 8, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1700.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (49, 1, 2, '250', 1000.00, 2, 5, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1500.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (50, 2, 2, '251', 600.00, 2, 4, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (51, 1, 2, '252', 1000.00, 2, 5, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1500.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (52, 1, 2, '253', 1000.00, 2, 5, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1500.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (53, 2, 2, '254', 600.00, 2, 5, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (54, 2, 2, '255', 600.00, 2, 5, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (55, 2, 2, '256', 700.00, 2, 5, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (56, 2, 2, '257', 1300.00, 4, 5, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1800.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (57, 2, 2, '258', 700.00, 2, 5, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (58, 2, 2, '260', 600.00, 2, 5, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (59, 2, 2, '261', 600.00, 2, 5, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (60, 1, 2, '262', 1000.00, 2, 5, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1500.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (61, 2, 2, '263', 600.00, 2, 5, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (62, 2, 2, '264', 600.00, 2, 5, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (63, 2, 2, '265', 600.00, 2, 5, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (64, 2, 2, '266', 600.00, 2, 5, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (65, 2, 2, '267', 600.00, 2, 5, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (66, 2, 2, '268', 600.00, 2, 5, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (67, 2, 2, '269', 600.00, 2, 5, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (68, 2, 2, '270', 600.00, 2, 4, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (69, 2, 2, '271', 600.00, 2, 4, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (70, 2, 2, '272', 600.00, 2, 4, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (71, 2, 2, '273', 600.00, 2, 4, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (72, 2, 2, '274', 600.00, 2, 4, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (73, 2, 2, '275', 600.00, 2, 4, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (74, 1, 2, '276', 1000.00, 2, 4, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1500.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (75, 2, 2, '277', 600.00, 2, 4, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (76, 2, 2, '278', 600.00, 2, 4, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (77, 2, 2, '279', 600.00, 2, 4, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (78, 2, 2, '280', 600.00, 2, 5, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (79, 6, 2, '281', 11116.00, 0, 50, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 11616.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (80, 1, 2, '282', 2300.00, 8, 10, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 2800.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (81, 1, 3, '301', 1000.00, 2, 4, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1500.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (82, 1, 3, '302', 1000.00, 2, 4, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1500.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (83, 1, 3, '303', 1000.00, 2, 4, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1500.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (84, 1, 3, '304', 1000.00, 2, 4, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1500.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (85, 1, 3, '305', 1000.00, 2, 4, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1500.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (86, 1, 3, '306', 1000.00, 2, 4, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1500.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (87, 1, 3, '307', 1000.00, 2, 4, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1500.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (88, 1, 3, '308', 1000.00, 2, 4, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1500.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (89, 1, 3, '309', 1000.00, 2, 4, 1, 0, 'admin', '2026-05-03 06:39:33.63', 1100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (90, 1, 3, '310', 1000.00, 2, 4, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1500.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (91, 1, 3, '311', 1000.00, 2, 4, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1500.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (92, 1, 3, '312', 1000.00, 2, 4, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1500.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (93, 1, 3, '313', 1000.00, 2, 4, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1500.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (94, 1, 3, '314', 1000.00, 2, 4, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1500.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (95, 1, 3, '315', 1000.00, 2, 4, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1500.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (96, 1, 3, '316', 1000.00, 2, 4, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1500.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (97, 1, 3, '317', 1000.00, 2, 4, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1500.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (98, 1, 3, '318', 1000.00, 2, 4, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1500.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (99, 1, 3, '319', 1000.00, 2, 4, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1500.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (100, 2, 3, '320', 1000.00, 2, 2, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1500.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (101, 1, 3, '321', 1000.00, 2, 4, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1500.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (102, 1, 3, '322', 1000.00, 2, 4, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1500.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (103, 1, 3, '323', 1000.00, 2, 4, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1500.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (104, 1, 3, '324', 1000.00, 2, 4, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1500.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (105, 1, 3, '325', 1000.00, 2, 4, 1, 0, 'admin', '2026-05-03 06:39:33.63', 1500.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (106, 1, 3, '326', 1000.00, 2, 4, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1500.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (107, 1, 3, '327', 1000.00, 2, 4, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1500.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (108, 1, 3, '328', 1000.00, 2, 4, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1500.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (109, 1, 3, '329', 1000.00, 2, 4, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1500.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (110, 1, 3, '330', 1000.00, 2, 5, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1500.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (111, 1, 3, '391', 1100.00, 2, 6, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1600.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (112, 1, 3, '392', 1300.00, 4, 8, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1800.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (113, 1, 3, '393', 1000.00, 2, 6, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1500.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (114, 1, 3, '394', 1300.00, 4, 8, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1800.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (115, 1, 3, '395', 1100.00, 3, 5, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1600.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (116, 2, 3, '396', 5000.00, 0, 25, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 5500.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (117, 2, 3, '397', 1100.00, 3, 5, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 1600.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (118, 5, 4, '401', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (119, 5, 4, '402', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (120, 5, 4, '403', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (121, 5, 4, '404', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (122, 5, 4, '405', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (123, 5, 4, '406', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (124, 5, 4, '407', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (125, 5, 4, '408', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (126, 5, 4, '409', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (127, 5, 4, '410', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (128, 5, 4, '411', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (129, 5, 4, '412', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (130, 5, 4, '413', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (131, 5, 4, '414', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (132, 5, 4, '415', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (133, 5, 4, '416', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (134, 5, 4, '417', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (135, 5, 4, '418', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (136, 5, 4, '419', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (137, 5, 4, '420', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (138, 5, 4, '421', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (139, 5, 4, '422', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (140, 5, 4, '423', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (141, 5, 4, '424', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (142, 5, 4, '425', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (143, 5, 4, '426', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (144, 5, 4, '427', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (145, 5, 4, '428', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (146, 5, 4, '429', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (147, 5, 4, '430', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (148, 5, 4, '431', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (149, 5, 4, '432', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (150, 5, 4, '433', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (151, 5, 4, '434', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (152, 5, 4, '435', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (153, 5, 4, '436', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (154, 5, 4, '437', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (155, 5, 4, '438', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (156, 5, 4, '439', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (157, 5, 4, '440', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (158, 5, 4, '441', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (159, 5, 4, '442', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (160, 5, 4, '443', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (161, 5, 4, '444', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (162, 5, 4, '445', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (163, 5, 4, '446', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (164, 5, 4, '447', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (165, 5, 4, '448', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (166, 5, 4, '449', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (167, 5, 4, '450', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (168, 5, 4, '451', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (169, 5, 4, '452', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (170, 5, 4, '453', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (171, 5, 4, '454', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (172, 5, 4, '455', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (173, 5, 4, '456', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (174, 5, 4, '457', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (175, 5, 4, '458', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (176, 5, 4, '459', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (177, 5, 4, '460', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (178, 5, 4, '461', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (179, 5, 4, '462', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (180, 5, 4, '463', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (181, 5, 4, '464', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (182, 5, 4, '465', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (183, 5, 4, '466', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (184, 5, 4, '467', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (185, 5, 4, '468', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (186, 5, 4, '469', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (187, 5, 4, '470', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (188, 5, 4, '471', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (189, 5, 4, '472', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (190, 5, 4, '473', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (191, 5, 4, '474', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (192, 5, 4, '475', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (193, 5, 4, '476', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (194, 5, 4, '477', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (195, 5, 4, '478', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (196, 5, 4, '479', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (197, 5, 4, '480', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (198, 5, 4, '481', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (199, 5, 4, '482', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (200, 5, 4, '483', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (201, 5, 4, '484', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (202, 5, 4, '485', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (203, 5, 4, '486', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (204, 5, 4, '487', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (205, 5, 4, '488', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (206, 5, 4, '489', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (207, 5, 4, '490', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (208, 5, 4, '491', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (209, 5, 4, '492', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (210, 5, 4, '493', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (211, 5, 4, '494', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (212, 5, 4, '495', 50.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 100.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (213, 4, 4, '501', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (214, 4, 4, '502', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (215, 4, 4, '503', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (216, 4, 4, '504', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (217, 4, 4, '505', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (218, 4, 4, '506', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (219, 4, 4, '507', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (220, 4, 4, '508', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (221, 4, 4, '509', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (222, 4, 4, '510', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (223, 4, 4, '511', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (224, 4, 4, '512', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (225, 4, 4, '513', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (226, 4, 4, '514', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (227, 4, 4, '515', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (228, 4, 4, '516', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (229, 4, 4, '517', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (230, 4, 4, '518', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (231, 4, 4, '519', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (232, 4, 4, '520', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (233, 4, 4, '521', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (234, 4, 4, '522', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (235, 4, 4, '523', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (236, 4, 4, '524', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (237, 4, 4, '525', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (238, 4, 4, '526', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (239, 4, 4, '527', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (240, 4, 4, '528', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (241, 4, 4, '529', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (242, 4, 4, '530', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (243, 4, 4, '531', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (244, 4, 4, '532', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (245, 4, 4, '533', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (246, 4, 4, '534', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (247, 4, 4, '535', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (248, 4, 4, '536', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (249, 4, 4, '537', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (250, 4, 4, '538', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (251, 4, 4, '539', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (252, 4, 4, '540', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (253, 4, 4, '541', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (254, 4, 4, '542', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (255, 4, 4, '543', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (256, 4, 4, '544', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (257, 4, 4, '545', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (258, 4, 4, '546', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (259, 4, 4, '547', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (260, 4, 4, '548', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (261, 4, 4, '549', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (262, 4, 4, '550', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (263, 4, 4, '551', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (264, 4, 4, '552', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (265, 4, 4, '553', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (266, 4, 4, '554', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (267, 4, 4, '555', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (268, 4, 4, '556', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (269, 4, 4, '557', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (270, 4, 4, '558', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (271, 4, 4, '559', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (272, 4, 4, '560', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (273, 4, 4, '561', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (274, 4, 4, '562', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (275, 4, 4, '563', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (276, 4, 4, '564', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (277, 4, 4, '565', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (278, 4, 4, '566', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (279, 4, 4, '567', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (280, 4, 4, '568', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (281, 4, 4, '569', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (282, 4, 4, '570', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (283, 4, 4, '571', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (284, 4, 4, '572', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (285, 4, 4, '573', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (286, 4, 4, '574', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (287, 4, 4, '575', 150.00, 0, 1, 1, 0, 'Admin', '2026-05-03 06:39:33.63', 200.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (289, 1, 4, '1001', 1000.00, 8, 10, 1, 0, 'Admin', '2026-05-29 12:48:47.713', 1500.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (290, 3, 2, '1002', 1200.00, 4, 6, 1, 0, 'admin', '2026-05-29 13:17:17.97', 1500.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (291, 1, 1, 'Pent House', 1500.00, 2, 3, 1, 0, 'admin', '2026-06-09 13:20:32.29', 3000.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (292, 1, 4, '786', 4000.00, 8, 10, 1, 0, 'admin', '2026-06-11 15:16:05.947', 6000.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (293, 0, 0, 'AUTO-20781', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-03 12:46:55.573', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (294, 0, 0, 'AUTO-55839', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-03 12:52:23.803', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (295, 0, 0, 'AUTO-90550', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-03 12:59:52.197', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (296, 0, 0, 'AUTO-90401', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-03 13:06:45.337', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (297, 0, 0, 'AUTO-53437', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-03 13:12:11.887', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (298, 0, 0, 'AUTO-58907', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-03 13:13:57.547', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (299, 0, 0, 'AUTO-18491292', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-03 13:19:14.663', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (300, 0, 0, 'AUTO-18524796', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-03 13:22:49.73', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (301, 0, 0, 'AUTO-18544826', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-03 13:24:50.04', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (302, 0, 0, 'AUTO-18571498', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-03 13:27:16.673', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (303, 0, 0, 'AUTO-19015496', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-03 13:31:56.857', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (304, 0, 0, 'AUTO-19044278', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-03 13:34:44.357', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (305, 0, 0, 'AUTO-19331483', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-03 14:03:16.62', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (306, 0, 0, 'AUTO-19353018', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-03 14:05:32.23', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (307, 0, 0, 'AUTO-19522001', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-03 14:22:21.987', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (308, 0, 0, 'AUTO-19585156', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-03 14:28:53.427', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (309, 0, 0, 'AUTO-22245876', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-03 16:55:06.653', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (310, 0, 0, 'AUTO-22273455', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-03 16:57:41.983', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (311, 0, 0, 'AUTO-22304824', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-03 17:00:56.01', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (312, 0, 0, 'AUTO-22335579', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-03 17:04:03.59', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (313, 0, 0, 'AUTO-23135583', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-03 17:44:04.623', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (314, 0, 0, 'AUTO-00230445', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-03 18:53:09.587', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (315, 0, 0, 'AUTO-00301881', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-03 19:00:24.523', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (316, 0, 0, 'AUTO-07044218', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-04 01:34:47.537', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (317, 0, 0, 'AUTO-09203705', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-04 03:50:41.173', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (318, 0, 0, 'AUTO-09240714', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-04 03:54:11.267', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (319, 0, 0, 'AUTO-09283830', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-04 03:58:42.043', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (320, 0, 0, 'AUTO-09574045', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-04 04:27:44.68', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (321, 0, 0, 'AUTO-10133925', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-04 04:43:43.487', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (322, 0, 0, 'AUTO-12272098', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-04 06:57:24.633', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (323, 0, 0, 'AUTO-12284714', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-04 06:58:50.613', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (324, 0, 0, 'AUTO-14305257', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-04 09:00:56.12', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (325, 0, 0, 'AUTO-22052696', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-04 16:35:30.16', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (326, 0, 0, 'AUTO-22374312', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-04 17:07:46.04', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (327, 0, 0, 'AUTO-17224630', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-05 11:52:49.603', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (328, 0, 0, 'AUTO-21543411', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-05 16:24:37.2', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (329, 0, 0, 'AUTO-23512593', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-05 18:21:28.707', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (330, 0, 0, 'AUTO-23582974', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-05 18:28:32.6', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (331, 0, 0, 'AUTO-00011836', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-05 18:31:21.22', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (332, 0, 0, 'AUTO-08582948', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-06 03:28:31.933', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (333, 0, 0, 'AUTO-15222575', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-06 09:52:27.717', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (334, 0, 0, 'AUTO-15560994', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-06 10:26:11.88', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (335, 0, 0, 'AUTO-17011566', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-06 11:31:17.5', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (336, 0, 0, 'AUTO-17155705', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-06 11:45:58.813', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (337, 0, 0, 'AUTO-17334245', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-06 12:03:44.83', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (338, 0, 0, 'AUTO-19413784', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-06 14:11:39.537', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (339, 0, 0, 'AUTO-20084519', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-06 14:38:47.02', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (340, 0, 0, 'AUTO-20121641', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-06 14:42:18.033', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (341, 0, 0, 'AUTO-20272935', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-06 14:57:31.307', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (342, 0, 0, 'AUTO-20301784', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-06 15:00:20.093', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (343, 0, 0, 'AUTO-20340071', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-06 15:04:02.407', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (344, 0, 0, 'AUTO-20400869', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-06 15:10:11.747', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (345, 0, 0, 'AUTO-22253510', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-09 16:55:40.907', 999.00);
INSERT INTO public."tbl_Room" ("Room_ID", "RT_ID", "Floor_ID", "Room_No", "Cost", "Beds", "Allowed", "Room_Status_ID", "Booking_ID", "Created_By", "Created_Timestamp", "AdvanceAmount") VALUES (346, 0, 0, 'AUTO-22305034', 999.00, 1, 1, 1, 0, 'uitest', '2026-07-09 17:00:56.133', 999.00);


--
-- Data for Name: tbl_Room_Status; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."tbl_Room_Status" ("Room_Status_ID", "Status_Code", "Room_Status_Desc", "Notes", "Created_By", "Created_Timestamp") VALUES (1, 'AV', 'Available', 'Free for booking', 'Admin', '2026-05-12 05:56:55.227');
INSERT INTO public."tbl_Room_Status" ("Room_Status_ID", "Status_Code", "Room_Status_Desc", "Notes", "Created_By", "Created_Timestamp") VALUES (2, 'OC', 'Occupied', 'Guest currently staying', 'Admin', '2026-05-12 05:57:13.63');
INSERT INTO public."tbl_Room_Status" ("Room_Status_ID", "Status_Code", "Room_Status_Desc", "Notes", "Created_By", "Created_Timestamp") VALUES (3, 'VC', 'Vacant', 'Guest checked out, cleaning pending', 'Admin', '2026-05-12 05:57:35.76');
INSERT INTO public."tbl_Room_Status" ("Room_Status_ID", "Status_Code", "Room_Status_Desc", "Notes", "Created_By", "Created_Timestamp") VALUES (4, 'CL', 'Cleaning', 'Ready for next guest', 'Admin', '2026-05-12 05:57:54.763');
INSERT INTO public."tbl_Room_Status" ("Room_Status_ID", "Status_Code", "Room_Status_Desc", "Notes", "Created_By", "Created_Timestamp") VALUES (5, 'MT', 'Maintenance', 'Blocked for repairs or service', 'Admin', '2026-05-12 05:58:18.173');
INSERT INTO public."tbl_Room_Status" ("Room_Status_ID", "Status_Code", "Room_Status_Desc", "Notes", "Created_By", "Created_Timestamp") VALUES (6, 'BL', 'Reserved', 'Temporarily held (VIP, staff use)
', 'Admin', '2026-05-12 05:58:36.097');


--
-- Data for Name: tbl_Room_Type; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."tbl_Room_Type" ("RT_ID", "RT_Desc", "Room_Category", "RT_Created_By", "RT_Created_Timestamp", is_active) VALUES (1, 'AirConditionar', 'AC', 'Admin', '2026-05-03 06:09:31.67', 'Y');
INSERT INTO public."tbl_Room_Type" ("RT_ID", "RT_Desc", "Room_Category", "RT_Created_By", "RT_Created_Timestamp", is_active) VALUES (2, 'Non AC', 'NON', 'Admin', '2026-05-03 06:09:53.23', 'Y');
INSERT INTO public."tbl_Room_Type" ("RT_ID", "RT_Desc", "Room_Category", "RT_Created_By", "RT_Created_Timestamp", is_active) VALUES (3, 'Luxury Room', 'LUX', 'Admin', '2026-05-03 06:10:35.547', 'N');
INSERT INTO public."tbl_Room_Type" ("RT_ID", "RT_Desc", "Room_Category", "RT_Created_By", "RT_Created_Timestamp", is_active) VALUES (4, 'Dormentary', 'DOR', 'Admin', '2026-05-03 06:10:42.733', 'Y');
INSERT INTO public."tbl_Room_Type" ("RT_ID", "RT_Desc", "Room_Category", "RT_Created_By", "RT_Created_Timestamp", is_active) VALUES (5, 'Lockers', 'LOC', 'Admin', '2026-05-03 06:12:07.723', 'Y');
INSERT INTO public."tbl_Room_Type" ("RT_ID", "RT_Desc", "Room_Category", "RT_Created_By", "RT_Created_Timestamp", is_active) VALUES (6, 'Function Hall', 'KL', 'Admin', '2026-05-03 06:29:38.45', 'Y');


--
-- Data for Name: tbl_SMSTemplate; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."tbl_SMSTemplate" ("SMSTemplateID", "SMSContent", "OccassionID", "Created_By", "Created_Timestamp") VALUES (1, 'Welcome to ABPS', 1, 'Admin', '2026-05-14 17:54:48.31');
INSERT INTO public."tbl_SMSTemplate" ("SMSTemplateID", "SMSContent", "OccassionID", "Created_By", "Created_Timestamp") VALUES (2, 'Welcome to ABPS', 2, NULL, '2026-05-14 17:54:52.6');
INSERT INTO public."tbl_SMSTemplate" ("SMSTemplateID", "SMSContent", "OccassionID", "Created_By", "Created_Timestamp") VALUES (3, 'Welcome to ABPS', 3, NULL, '2026-05-14 17:54:56.95');
INSERT INTO public."tbl_SMSTemplate" ("SMSTemplateID", "SMSContent", "OccassionID", "Created_By", "Created_Timestamp") VALUES (4, 'Welcome to ABPS', 4, NULL, '2026-05-14 17:55:03.533');
INSERT INTO public."tbl_SMSTemplate" ("SMSTemplateID", "SMSContent", "OccassionID", "Created_By", "Created_Timestamp") VALUES (5, 'Welcome to ABPS', 5, NULL, '2026-05-14 17:55:12.827');
INSERT INTO public."tbl_SMSTemplate" ("SMSTemplateID", "SMSContent", "OccassionID", "Created_By", "Created_Timestamp") VALUES (6, 'Welcome to ABPS', 6, NULL, '2026-05-14 17:55:21.65');
INSERT INTO public."tbl_SMSTemplate" ("SMSTemplateID", "SMSContent", "OccassionID", "Created_By", "Created_Timestamp") VALUES (7, 'Welcome to ABPS', 7, NULL, '2026-05-14 17:55:38.13');


--
-- Data for Name: tbl_States; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."tbl_States" ("StateID", "CountryID", "StateName", "Created_By", "Created_Timestamp") VALUES (1, 1, 'Andhra Pradesh', 'system', '2026-05-17 16:53:10.297');
INSERT INTO public."tbl_States" ("StateID", "CountryID", "StateName", "Created_By", "Created_Timestamp") VALUES (2, 1, 'Arunachal Pradesh', 'system', '2026-05-17 16:53:10.297');
INSERT INTO public."tbl_States" ("StateID", "CountryID", "StateName", "Created_By", "Created_Timestamp") VALUES (3, 1, 'Assam', 'system', '2026-05-17 16:53:10.297');
INSERT INTO public."tbl_States" ("StateID", "CountryID", "StateName", "Created_By", "Created_Timestamp") VALUES (4, 1, 'Bihar', 'system', '2026-05-17 16:53:10.297');
INSERT INTO public."tbl_States" ("StateID", "CountryID", "StateName", "Created_By", "Created_Timestamp") VALUES (5, 1, 'Chhattisgarh', 'system', '2026-05-17 16:53:10.297');
INSERT INTO public."tbl_States" ("StateID", "CountryID", "StateName", "Created_By", "Created_Timestamp") VALUES (6, 1, 'Goa', 'system', '2026-05-17 16:53:10.297');
INSERT INTO public."tbl_States" ("StateID", "CountryID", "StateName", "Created_By", "Created_Timestamp") VALUES (7, 1, 'Gujarat', 'system', '2026-05-17 16:53:10.297');
INSERT INTO public."tbl_States" ("StateID", "CountryID", "StateName", "Created_By", "Created_Timestamp") VALUES (8, 1, 'Haryana', 'system', '2026-05-17 16:53:10.297');
INSERT INTO public."tbl_States" ("StateID", "CountryID", "StateName", "Created_By", "Created_Timestamp") VALUES (9, 1, 'Himachal Pradesh', 'system', '2026-05-17 16:53:10.297');
INSERT INTO public."tbl_States" ("StateID", "CountryID", "StateName", "Created_By", "Created_Timestamp") VALUES (10, 1, 'Jharkhand', 'system', '2026-05-17 16:53:10.297');
INSERT INTO public."tbl_States" ("StateID", "CountryID", "StateName", "Created_By", "Created_Timestamp") VALUES (11, 1, 'Karnataka', 'system', '2026-05-17 16:53:10.297');
INSERT INTO public."tbl_States" ("StateID", "CountryID", "StateName", "Created_By", "Created_Timestamp") VALUES (12, 1, 'Kerala', 'system', '2026-05-17 16:53:10.297');
INSERT INTO public."tbl_States" ("StateID", "CountryID", "StateName", "Created_By", "Created_Timestamp") VALUES (13, 1, 'Madhya Pradesh', 'system', '2026-05-17 16:53:10.297');
INSERT INTO public."tbl_States" ("StateID", "CountryID", "StateName", "Created_By", "Created_Timestamp") VALUES (14, 1, 'Maharashtra', 'system', '2026-05-17 16:53:10.297');
INSERT INTO public."tbl_States" ("StateID", "CountryID", "StateName", "Created_By", "Created_Timestamp") VALUES (15, 1, 'Manipur', 'system', '2026-05-17 16:53:10.297');
INSERT INTO public."tbl_States" ("StateID", "CountryID", "StateName", "Created_By", "Created_Timestamp") VALUES (16, 1, 'Meghalaya', 'system', '2026-05-17 16:53:10.297');
INSERT INTO public."tbl_States" ("StateID", "CountryID", "StateName", "Created_By", "Created_Timestamp") VALUES (17, 1, 'Mizoram', 'system', '2026-05-17 16:53:10.297');
INSERT INTO public."tbl_States" ("StateID", "CountryID", "StateName", "Created_By", "Created_Timestamp") VALUES (18, 1, 'Nagaland', 'system', '2026-05-17 16:53:10.297');
INSERT INTO public."tbl_States" ("StateID", "CountryID", "StateName", "Created_By", "Created_Timestamp") VALUES (19, 1, 'Odisha', 'system', '2026-05-17 16:53:10.297');
INSERT INTO public."tbl_States" ("StateID", "CountryID", "StateName", "Created_By", "Created_Timestamp") VALUES (20, 1, 'Punjab', 'system', '2026-05-17 16:53:10.297');
INSERT INTO public."tbl_States" ("StateID", "CountryID", "StateName", "Created_By", "Created_Timestamp") VALUES (21, 1, 'Rajasthan', 'system', '2026-05-17 16:53:10.297');
INSERT INTO public."tbl_States" ("StateID", "CountryID", "StateName", "Created_By", "Created_Timestamp") VALUES (22, 1, 'Sikkim', 'system', '2026-05-17 16:53:10.297');
INSERT INTO public."tbl_States" ("StateID", "CountryID", "StateName", "Created_By", "Created_Timestamp") VALUES (23, 1, 'Tamil Nadu', 'system', '2026-05-17 16:53:10.297');
INSERT INTO public."tbl_States" ("StateID", "CountryID", "StateName", "Created_By", "Created_Timestamp") VALUES (24, 1, 'Telangana', 'system', '2026-05-17 16:53:10.297');
INSERT INTO public."tbl_States" ("StateID", "CountryID", "StateName", "Created_By", "Created_Timestamp") VALUES (25, 1, 'Tripura', 'system', '2026-05-17 16:53:10.297');
INSERT INTO public."tbl_States" ("StateID", "CountryID", "StateName", "Created_By", "Created_Timestamp") VALUES (26, 1, 'Uttar Pradesh', 'system', '2026-05-17 16:53:10.297');
INSERT INTO public."tbl_States" ("StateID", "CountryID", "StateName", "Created_By", "Created_Timestamp") VALUES (27, 1, 'Uttarakhand', 'system', '2026-05-17 16:53:10.297');
INSERT INTO public."tbl_States" ("StateID", "CountryID", "StateName", "Created_By", "Created_Timestamp") VALUES (28, 1, 'West Bengal', 'system', '2026-05-17 16:53:10.297');
INSERT INTO public."tbl_States" ("StateID", "CountryID", "StateName", "Created_By", "Created_Timestamp") VALUES (29, 1, 'Andaman and Nicobar Islands', 'system', '2026-05-17 16:53:10.297');
INSERT INTO public."tbl_States" ("StateID", "CountryID", "StateName", "Created_By", "Created_Timestamp") VALUES (30, 1, 'Chandigarh', 'system', '2026-05-17 16:53:10.297');
INSERT INTO public."tbl_States" ("StateID", "CountryID", "StateName", "Created_By", "Created_Timestamp") VALUES (31, 1, 'Dadra and Nagar Haveli and Daman and Diu', 'system', '2026-05-17 16:53:10.297');
INSERT INTO public."tbl_States" ("StateID", "CountryID", "StateName", "Created_By", "Created_Timestamp") VALUES (32, 1, 'Delhi', 'system', '2026-05-17 16:53:10.297');
INSERT INTO public."tbl_States" ("StateID", "CountryID", "StateName", "Created_By", "Created_Timestamp") VALUES (33, 1, 'Jammu and Kashmir', 'system', '2026-05-17 16:53:10.297');
INSERT INTO public."tbl_States" ("StateID", "CountryID", "StateName", "Created_By", "Created_Timestamp") VALUES (34, 1, 'Ladakh', 'system', '2026-05-17 16:53:10.297');
INSERT INTO public."tbl_States" ("StateID", "CountryID", "StateName", "Created_By", "Created_Timestamp") VALUES (35, 1, 'Lakshadweep', 'system', '2026-05-17 16:53:10.297');
INSERT INTO public."tbl_States" ("StateID", "CountryID", "StateName", "Created_By", "Created_Timestamp") VALUES (36, 1, 'Puducherry', 'system', '2026-05-17 16:53:10.297');
INSERT INTO public."tbl_States" ("StateID", "CountryID", "StateName", "Created_By", "Created_Timestamp") VALUES (37, 0, 'Other', 'Admin', '2026-05-17 16:53:10.297');


--
-- Data for Name: tbl_TeluguMasam; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."tbl_TeluguMasam" ("T_MasamID", "T_MasamName", "T_MasamName_English", "Created_By", "Created_Timestamp") VALUES (1, '??????', 'Chaitram', 'Admin', '2026-05-02 10:21:04.44');
INSERT INTO public."tbl_TeluguMasam" ("T_MasamID", "T_MasamName", "T_MasamName_English", "Created_By", "Created_Timestamp") VALUES (2, '??????', 'Vaisakham', NULL, '2026-05-02 10:21:06.327');
INSERT INTO public."tbl_TeluguMasam" ("T_MasamID", "T_MasamName", "T_MasamName_English", "Created_By", "Created_Timestamp") VALUES (3, '????????', 'Jyeshtham', NULL, '2026-05-02 10:21:19.35');
INSERT INTO public."tbl_TeluguMasam" ("T_MasamID", "T_MasamName", "T_MasamName_English", "Created_By", "Created_Timestamp") VALUES (4, '?????', 'Ashadham', NULL, '2026-05-02 10:21:24.74');
INSERT INTO public."tbl_TeluguMasam" ("T_MasamID", "T_MasamName", "T_MasamName_English", "Created_By", "Created_Timestamp") VALUES (5, '???????', 'Sravanam', NULL, '2026-05-02 10:21:35.797');
INSERT INTO public."tbl_TeluguMasam" ("T_MasamID", "T_MasamName", "T_MasamName_English", "Created_By", "Created_Timestamp") VALUES (6, '????????', 'Bhadrapadam', NULL, '2026-05-02 10:21:41.42');
INSERT INTO public."tbl_TeluguMasam" ("T_MasamID", "T_MasamName", "T_MasamName_English", "Created_By", "Created_Timestamp") VALUES (7, '?????????', 'Ashwayujam', NULL, '2026-05-02 10:21:48.303');
INSERT INTO public."tbl_TeluguMasam" ("T_MasamID", "T_MasamName", "T_MasamName_English", "Created_By", "Created_Timestamp") VALUES (8, '????????', 'Karthikam', NULL, '2026-05-02 10:21:54.647');
INSERT INTO public."tbl_TeluguMasam" ("T_MasamID", "T_MasamName", "T_MasamName_English", "Created_By", "Created_Timestamp") VALUES (9, '?????????', 'Margasiram', NULL, '2026-05-02 10:22:00.23');
INSERT INTO public."tbl_TeluguMasam" ("T_MasamID", "T_MasamName", "T_MasamName_English", "Created_By", "Created_Timestamp") VALUES (10, '??????', 'Pushyam', NULL, '2026-05-02 10:22:06.763');
INSERT INTO public."tbl_TeluguMasam" ("T_MasamID", "T_MasamName", "T_MasamName_English", "Created_By", "Created_Timestamp") VALUES (11, '????', 'Magham', NULL, '2026-05-02 10:22:14.017');
INSERT INTO public."tbl_TeluguMasam" ("T_MasamID", "T_MasamName", "T_MasamName_English", "Created_By", "Created_Timestamp") VALUES (12, '????????', 'Phalgunam', NULL, '2026-05-02 10:22:22.29');


--
-- Data for Name: tbl_TeluguPaksham; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."tbl_TeluguPaksham" ("T_PakshamID", "T_PakshamName", "T_PakshamName_English", "Created_By", "Created_Timestamp") VALUES (1, '?????', 'Shukla', NULL, '2026-05-02 10:24:30.78');
INSERT INTO public."tbl_TeluguPaksham" ("T_PakshamID", "T_PakshamName", "T_PakshamName_English", "Created_By", "Created_Timestamp") VALUES (2, '?????', 'Krishna', NULL, '2026-05-02 10:24:36.92');


--
-- Data for Name: tbl_TeluguThidi; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."tbl_TeluguThidi" ("T_ThidiID", "T_ThidiName", "T_ThidiName_English", "Created_By", "Created_Timestamp") VALUES (1, '???????', 'Pournami', 'Admin', '2026-05-02 10:25:43.52');
INSERT INTO public."tbl_TeluguThidi" ("T_ThidiID", "T_ThidiName", "T_ThidiName_English", "Created_By", "Created_Timestamp") VALUES (2, '???????', 'Padyami', NULL, '2026-05-02 10:25:48.063');
INSERT INTO public."tbl_TeluguThidi" ("T_ThidiID", "T_ThidiName", "T_ThidiName_English", "Created_By", "Created_Timestamp") VALUES (3, '?????', 'Vidiya', NULL, '2026-05-02 10:25:56.33');
INSERT INTO public."tbl_TeluguThidi" ("T_ThidiID", "T_ThidiName", "T_ThidiName_English", "Created_By", "Created_Timestamp") VALUES (4, '????', 'Thadiya', NULL, '2026-05-02 10:26:02.757');
INSERT INTO public."tbl_TeluguThidi" ("T_ThidiID", "T_ThidiName", "T_ThidiName_English", "Created_By", "Created_Timestamp") VALUES (5, '?????', 'Chavithi', NULL, '2026-05-02 10:26:08.717');
INSERT INTO public."tbl_TeluguThidi" ("T_ThidiID", "T_ThidiName", "T_ThidiName_English", "Created_By", "Created_Timestamp") VALUES (6, '?????', 'Panchami', NULL, '2026-05-02 10:26:13.22');
INSERT INTO public."tbl_TeluguThidi" ("T_ThidiID", "T_ThidiName", "T_ThidiName_English", "Created_By", "Created_Timestamp") VALUES (7, '?????', 'Shashti', NULL, '2026-05-02 10:26:18.07');
INSERT INTO public."tbl_TeluguThidi" ("T_ThidiID", "T_ThidiName", "T_ThidiName_English", "Created_By", "Created_Timestamp") VALUES (8, '??????', 'Saptami', NULL, '2026-05-02 10:26:25.447');
INSERT INTO public."tbl_TeluguThidi" ("T_ThidiID", "T_ThidiName", "T_ThidiName_English", "Created_By", "Created_Timestamp") VALUES (9, '??????', 'Ashtami', NULL, '2026-05-02 10:26:32.417');
INSERT INTO public."tbl_TeluguThidi" ("T_ThidiID", "T_ThidiName", "T_ThidiName_English", "Created_By", "Created_Timestamp") VALUES (10, '????', 'Navami', NULL, '2026-05-02 10:26:39.13');
INSERT INTO public."tbl_TeluguThidi" ("T_ThidiID", "T_ThidiName", "T_ThidiName_English", "Created_By", "Created_Timestamp") VALUES (11, '????', 'Dasami', NULL, '2026-05-02 10:26:45.083');
INSERT INTO public."tbl_TeluguThidi" ("T_ThidiID", "T_ThidiName", "T_ThidiName_English", "Created_By", "Created_Timestamp") VALUES (12, '??????', 'Ekadashi', NULL, '2026-05-02 10:26:51.99');
INSERT INTO public."tbl_TeluguThidi" ("T_ThidiID", "T_ThidiName", "T_ThidiName_English", "Created_By", "Created_Timestamp") VALUES (13, '???????', 'Dwadashi', NULL, '2026-05-02 10:26:57.06');
INSERT INTO public."tbl_TeluguThidi" ("T_ThidiID", "T_ThidiName", "T_ThidiName_English", "Created_By", "Created_Timestamp") VALUES (14, '????????', 'Thrayodashi', NULL, '2026-05-02 10:27:02.533');
INSERT INTO public."tbl_TeluguThidi" ("T_ThidiID", "T_ThidiName", "T_ThidiName_English", "Created_By", "Created_Timestamp") VALUES (15, '????????', 'Chaturdashi', NULL, '2026-05-02 10:27:10.973');
INSERT INTO public."tbl_TeluguThidi" ("T_ThidiID", "T_ThidiName", "T_ThidiName_English", "Created_By", "Created_Timestamp") VALUES (16, '????????  ', 'Amavasya', NULL, '2026-05-02 10:27:20.32');


--
-- Data for Name: tbl_TrasactionCategory_Master; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."tbl_TrasactionCategory_Master" ("TrasactionCategoryID", "TransactionCategory", "Created_By", "Created_Timestamp") VALUES (1, 'RoomBooking', 'Admin', '2026-05-01 13:34:10.857');
INSERT INTO public."tbl_TrasactionCategory_Master" ("TrasactionCategoryID", "TransactionCategory", "Created_By", "Created_Timestamp") VALUES (2, 'Donation', 'Admin', '2026-05-01 13:34:24.81');
INSERT INTO public."tbl_TrasactionCategory_Master" ("TrasactionCategoryID", "TransactionCategory", "Created_By", "Created_Timestamp") VALUES (3, 'CAF', 'Admin', '2026-05-01 13:34:30.63');


--
-- Data for Name: tbl_TrasactionType_Master; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."tbl_TrasactionType_Master" ("TrasactionTypeID", "TransactionType", "Created_By", "Created_Timestamp") VALUES (1, 'Advance', 'Admin', '2026-05-01 13:33:40.693');
INSERT INTO public."tbl_TrasactionType_Master" ("TrasactionTypeID", "TransactionType", "Created_By", "Created_Timestamp") VALUES (2, 'Payment', 'Admin', '2026-05-01 13:33:44.54');
INSERT INTO public."tbl_TrasactionType_Master" ("TrasactionTypeID", "TransactionType", "Created_By", "Created_Timestamp") VALUES (3, 'Refund', 'Admin', '2026-05-01 13:33:50.377');


--
-- Data for Name: tbl_User; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."tbl_User" ("UserName", "UserID", "Email", "PasswordHash", "MobileNumber", "RoleId", "IsActive", "CreatedBy", "CreatedTimestamp", "UpdatedBy", "UpdatedTimestamp") VALUES ('Admin', 1, 'admin@abps.com', 'PBKDF2$100000$K273NscWeRUa4r6nqrzUeg==$nUXxYmXFSzsXN/DZ/g2ktm92isRZki+Hzo6P2NJDuXY=', '9160494929', 1, 'Y', 'Admin', '2026-05-22 17:24:26.357', NULL, '2026-05-22 17:24:26.357');


--
-- Data for Name: tbl_User_Role; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."tbl_User_Role" ("RoleID", "Role", created_by, created_timestamp) VALUES (1, 'Admin', 'Admin', '2026-05-22 16:57:27.843');
INSERT INTO public."tbl_User_Role" ("RoleID", "Role", created_by, created_timestamp) VALUES (2, 'User', 'Admin', '2026-05-22 16:57:36.21');
INSERT INTO public."tbl_User_Role" ("RoleID", "Role", created_by, created_timestamp) VALUES (3, 'Accountant', 'Admin', '2026-05-22 18:26:52.473');
INSERT INTO public."tbl_User_Role" ("RoleID", "Role", created_by, created_timestamp) VALUES (4, 'SuperUser', 'Admin', '2026-05-22 18:27:02.34');


--
-- Name: tbl_BookingCategory_CatID_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."tbl_BookingCategory_CatID_seq"', 5, false);


--
-- Name: tbl_BookingStatus_BookingStatusID_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."tbl_BookingStatus_BookingStatusID_seq"', 7, false);


--
-- Name: tbl_Country_CountryID_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."tbl_Country_CountryID_seq"', 4, false);


--
-- Name: tbl_Districts_DistrictID_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."tbl_Districts_DistrictID_seq"', 62, false);


--
-- Name: tbl_DonarType_Master_DTID_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."tbl_DonarType_Master_DTID_seq"', 62, false);


--
-- Name: tbl_Floor_Master_Floor_ID_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."tbl_Floor_Master_Floor_ID_seq"', 5, false);


--
-- Name: tbl_Occassion_Master_OccassionID_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."tbl_Occassion_Master_OccassionID_seq"', 8, false);


--
-- Name: tbl_PaymentSubTypeMaster_paymentSubTypeID_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."tbl_PaymentSubTypeMaster_paymentSubTypeID_seq"', 7, false);


--
-- Name: tbl_PaymentTypeMaster_paymentTypeID_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."tbl_PaymentTypeMaster_paymentTypeID_seq"', 4, false);


--
-- Name: tbl_Room_Room_ID_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."tbl_Room_Room_ID_seq"', 347, false);


--
-- Name: tbl_Room_Status_Room_Status_ID_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."tbl_Room_Status_Room_Status_ID_seq"', 7, false);


--
-- Name: tbl_Room_Type_RT_ID_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."tbl_Room_Type_RT_ID_seq"', 7, false);


--
-- Name: tbl_SMSTemplate_SMSTemplateID_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."tbl_SMSTemplate_SMSTemplateID_seq"', 8, false);


--
-- Name: tbl_States_StateID_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."tbl_States_StateID_seq"', 38, false);


--
-- Name: tbl_TeluguMasam_T_MasamID_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."tbl_TeluguMasam_T_MasamID_seq"', 13, false);


--
-- Name: tbl_TeluguPaksham_T_PakshamID_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."tbl_TeluguPaksham_T_PakshamID_seq"', 3, false);


--
-- Name: tbl_TeluguThidi_T_ThidiID_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."tbl_TeluguThidi_T_ThidiID_seq"', 17, false);


--
-- Name: tbl_TrasactionCategory_Master_TrasactionCategoryID_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."tbl_TrasactionCategory_Master_TrasactionCategoryID_seq"', 4, false);


--
-- Name: tbl_TrasactionType_Master_TrasactionTypeID_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."tbl_TrasactionType_Master_TrasactionTypeID_seq"', 4, false);


--
-- Name: tbl_User_Role_RoleID_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."tbl_User_Role_RoleID_seq"', 5, false);


--
-- Name: tbl_User_UserID_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."tbl_User_UserID_seq"', 56, false);


--
-- PostgreSQL database dump complete
--

\unrestrict BIWFxUr3lKfIg6elVkmVXEbruZ6zaxGySWUeVZzZMBjbhBPt0VvCS9XfaSymiaL

