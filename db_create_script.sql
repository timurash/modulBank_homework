--
-- PostgreSQL database dump
--

-- Dumped from database version 12.2
-- Dumped by pg_dump version 12.2

-- Started on 2020-05-13 00:47:01 +05

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

DROP DATABASE userstore3;
--
-- TOC entry 3181 (class 1262 OID 33225)
-- Name: userstore3; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE userstore3 WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'C' LC_CTYPE = 'C';


ALTER DATABASE userstore3 OWNER TO postgres;

\connect userstore3

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

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 205 (class 1259 OID 33273)
-- Name: accounts; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.accounts (
    accountnumber bigint NOT NULL,
    ownerid integer,
    accountname text,
    balance double precision
);


ALTER TABLE public.accounts OWNER TO postgres;

--
-- TOC entry 207 (class 1259 OID 33297)
-- Name: accountnumberseq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.accountnumberseq
    START WITH 4000000000
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.accountnumberseq OWNER TO postgres;

--
-- TOC entry 3182 (class 0 OID 0)
-- Dependencies: 207
-- Name: accountnumberseq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.accountnumberseq OWNED BY public.accounts.accountnumber;


--
-- TOC entry 204 (class 1259 OID 33271)
-- Name: accounts_accountnumber_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.accounts_accountnumber_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.accounts_accountnumber_seq OWNER TO postgres;

--
-- TOC entry 3183 (class 0 OID 0)
-- Dependencies: 204
-- Name: accounts_accountnumber_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.accounts_accountnumber_seq OWNED BY public.accounts.accountnumber;


--
-- TOC entry 208 (class 1259 OID 33311)
-- Name: transaction_types; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.transaction_types (
    name text,
    id integer NOT NULL
);


ALTER TABLE public.transaction_types OWNER TO postgres;

--
-- TOC entry 209 (class 1259 OID 33341)
-- Name: transaction_types_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.transaction_types_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.transaction_types_id_seq OWNER TO postgres;

--
-- TOC entry 3184 (class 0 OID 0)
-- Dependencies: 209
-- Name: transaction_types_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.transaction_types_id_seq OWNED BY public.transaction_types.id;


--
-- TOC entry 206 (class 1259 OID 33287)
-- Name: transactions; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.transactions (
    transactiontype integer,
    value double precision,
    fromaccount bigint,
    toaccount bigint,
    userid integer,
    id integer NOT NULL
);


ALTER TABLE public.transactions OWNER TO postgres;

--
-- TOC entry 210 (class 1259 OID 33375)
-- Name: transactions_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.transactions_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.transactions_id_seq OWNER TO postgres;

--
-- TOC entry 3185 (class 0 OID 0)
-- Dependencies: 210
-- Name: transactions_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.transactions_id_seq OWNED BY public.transactions.id;


--
-- TOC entry 203 (class 1259 OID 33262)
-- Name: users; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.users (
    id integer NOT NULL,
    username text,
    passwordhash text,
    salt text
);


ALTER TABLE public.users OWNER TO postgres;

--
-- TOC entry 202 (class 1259 OID 33260)
-- Name: users_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.users_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.users_id_seq OWNER TO postgres;

--
-- TOC entry 3186 (class 0 OID 0)
-- Dependencies: 202
-- Name: users_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.users_id_seq OWNED BY public.users.id;


--
-- TOC entry 3028 (class 2604 OID 33299)
-- Name: accounts accountnumber; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.accounts ALTER COLUMN accountnumber SET DEFAULT nextval('public.accounts_accountnumber_seq'::regclass);


--
-- TOC entry 3030 (class 2604 OID 33343)
-- Name: transaction_types id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transaction_types ALTER COLUMN id SET DEFAULT nextval('public.transaction_types_id_seq'::regclass);


--
-- TOC entry 3029 (class 2604 OID 33377)
-- Name: transactions id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transactions ALTER COLUMN id SET DEFAULT nextval('public.transactions_id_seq'::regclass);


--
-- TOC entry 3027 (class 2604 OID 33265)
-- Name: users id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users ALTER COLUMN id SET DEFAULT nextval('public.users_id_seq'::regclass);


--
-- TOC entry 3170 (class 0 OID 33273)
-- Dependencies: 205
-- Data for Name: accounts; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.accounts (accountnumber, ownerid, accountname, balance) VALUES (4000000001, 8, 'abcde', 600);
INSERT INTO public.accounts (accountnumber, ownerid, accountname, balance) VALUES (4000000002, 8, 'Основной счет 2', 500);
INSERT INTO public.accounts (accountnumber, ownerid, accountname, balance) VALUES (4000000003, 8, 'Основной счет 2', 500);
INSERT INTO public.accounts (accountnumber, ownerid, accountname, balance) VALUES (4000000004, 8, 'Основной счет 2', 500);
INSERT INTO public.accounts (accountnumber, ownerid, accountname, balance) VALUES (4000000005, 12, 'Основной счет 1', 5005);
INSERT INTO public.accounts (accountnumber, ownerid, accountname, balance) VALUES (4000000006, 12, 'Основной счет 2', 30005);
INSERT INTO public.accounts (accountnumber, ownerid, accountname, balance) VALUES (4000000007, 12, 'Основной счет 3', 5);


--
-- TOC entry 3173 (class 0 OID 33311)
-- Dependencies: 208
-- Data for Name: transaction_types; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.transaction_types (name, id) VALUES ('Пополнение счета', 1);
INSERT INTO public.transaction_types (name, id) VALUES ('Перевод денег на другой счет', 2);


--
-- TOC entry 3171 (class 0 OID 33287)
-- Dependencies: 206
-- Data for Name: transactions; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.transactions (transactiontype, value, fromaccount, toaccount, userid, id) VALUES (2, 25000, 4000000007, 4000000006, 12, 1);
INSERT INTO public.transactions (transactiontype, value, fromaccount, toaccount, userid, id) VALUES (2, 25000, 4000000006, 4000000007, 12, 2);


--
-- TOC entry 3168 (class 0 OID 33262)
-- Dependencies: 203
-- Data for Name: users; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.users (id, username, passwordhash, salt) VALUES (8, 'admin', '6CACE6EE7DEED3F7C105EF7ED6B55643', '24dd20c2');
INSERT INTO public.users (id, username, passwordhash, salt) VALUES (12, 'admin6', 'D7ADA6699DFE3D9D0C462D8568E25F6E', '98bb6ad5');
INSERT INTO public.users (id, username, passwordhash, salt) VALUES (4, 'testuser', 'A048CD0EC57E18992C842E0889DC5966', '434f098d');
INSERT INTO public.users (id, username, passwordhash, salt) VALUES (13, 'testuser1', 'FE07D93368F25B9D237E33D3F634F096', 'ec9f58b0');
INSERT INTO public.users (id, username, passwordhash, salt) VALUES (14, 'testuser2', 'C0E47BD5DD6D270A4F45B5CE61ABD2F8', '31473073');


--
-- TOC entry 3187 (class 0 OID 0)
-- Dependencies: 207
-- Name: accountnumberseq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.accountnumberseq', 4000000007, true);


--
-- TOC entry 3188 (class 0 OID 0)
-- Dependencies: 204
-- Name: accounts_accountnumber_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.accounts_accountnumber_seq', 2, true);


--
-- TOC entry 3189 (class 0 OID 0)
-- Dependencies: 209
-- Name: transaction_types_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.transaction_types_id_seq', 1, false);


--
-- TOC entry 3190 (class 0 OID 0)
-- Dependencies: 210
-- Name: transactions_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.transactions_id_seq', 2, true);


--
-- TOC entry 3191 (class 0 OID 0)
-- Dependencies: 202
-- Name: users_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.users_id_seq', 14, true);


--
-- TOC entry 3034 (class 2606 OID 33301)
-- Name: accounts accounts_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.accounts
    ADD CONSTRAINT accounts_pkey PRIMARY KEY (accountnumber);


--
-- TOC entry 3038 (class 2606 OID 33351)
-- Name: transaction_types transaction_types_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transaction_types
    ADD CONSTRAINT transaction_types_pkey PRIMARY KEY (id);


--
-- TOC entry 3036 (class 2606 OID 33382)
-- Name: transactions transactions_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transactions
    ADD CONSTRAINT transactions_pkey PRIMARY KEY (id);


--
-- TOC entry 3032 (class 2606 OID 33270)
-- Name: users users_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (id);


--
-- TOC entry 3039 (class 2606 OID 33357)
-- Name: accounts accounts; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.accounts
    ADD CONSTRAINT accounts FOREIGN KEY (ownerid) REFERENCES public.users(id) NOT VALID;


--
-- TOC entry 3040 (class 2606 OID 33362)
-- Name: transactions userid; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transactions
    ADD CONSTRAINT userid FOREIGN KEY (userid) REFERENCES public.users(id) NOT VALID;


-- Completed on 2020-05-13 00:47:01 +05

--
-- PostgreSQL database dump complete
--

