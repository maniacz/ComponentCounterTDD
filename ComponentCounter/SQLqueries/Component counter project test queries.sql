select h1.szuflada, count(pv.VALUE) ilosc, h1.itempartno
from (select rownum, partno, itempartno, 
		decode(itempartno, 'A0038109', '1', 'A0038110', '2', 'A0035512', '3', 'A0038271', '4', 'A0032886', '5', 'A0038111', '6', 'A0032887', '7', 'A0032888', '8') szuflada 
		from acc_bomitem_setup 
		where op = '140' and desc_local like 'O-%' and partno = '00519656480'
		order by itempartno) h1 
  left join
		ACC_PROCDATA_VALUE pv
	on h1.szuflada = pv.value
  and pv.DATAID = 248
	and pv.CTIME between '03.09.2019 09:14' and '03.09.2019 10:14'
--where pv.DATAID = (select pc.REC_ID from ACC_PROCDATA_CFG pc where pc.OP = '140' and pc.NAME = 'Drewer number')
group by h1.szuflada, h1.itempartno
order by h1.szuflada;


select itempartno,
decode(itempartno, 'A0038109', '1', 'A0038110', '2', 'A0035512', '3', 'A0038271', '4', 'A0032886', '5', 'A0038111', '6', 'A0032887', '7', 'A0032888', '8') szuflada 
from 
  (select op, itempartno, decode(itempartno, 'A0038109', '1', 'A0038110', '2', 'A0035512', '3', 'A0038271', '4', 'A0032886', '5', 'A0038111', '6', 'A0032887', '7', 'A0032888', '8') szuflada 
  from acc_bomitem_setup
  where op = '140')
where op = '140' and szuflada is not null
group by szuflada, itempartno
order by szuflada;

select h1.szuflada, count(pv.VALUE) ilosc, h1.itempartno
from (select itempartno,
decode(itempartno, 'A0038109', '1', 'A0038110', '2', 'A0035512', '3', 'A0038271', '4', 'A0032886', '5', 'A0038111', '6', 'A0032887', '7', 'A0032888', '8') szuflada 
from 
  (select op, itempartno, decode(itempartno, 'A0038109', '1', 'A0038110', '2', 'A0035512', '3', 'A0038271', '4', 'A0032886', '5', 'A0038111', '6', 'A0032887', '7', 'A0032888', '8') szuflada 
  from acc_bomitem_setup
  where op = '61')
where op = '61' and szuflada is not null
group by szuflada, itempartno) h1 
  left join
		ACC_PROCDATA_VALUE pv
	on h1.szuflada = pv.value
  and pv.DATAID in (947, 953, 959, 965, 971, 827) --B403
  --and pv.DATAID in (248) --6104
	and pv.CTIME between '11.09.2019 06:00' and '13.09.2019 06:00'
--where pv.DATAID = (select pc.REC_ID from ACC_PROCDATA_CFG pc where pc.OP = '140' and pc.NAME = 'Drewer number')
group by h1.szuflada, h1.itempartno
order by h1.szuflada;

select * from acc_unithistory where CTIME between '03.09.2019 14:14' and '03.09.2019 15:14' and op = '140';--6104
select * from acc_unithistory where CTIME between '03.09.2019 14:14' and '03.09.2019 15:14' and op = '140';--B403

select * from acc_unithistory where op = '61' and CTIME between '03.09.2019 09:14' and '03.09.2019 10:14';

select * from ACC_PROCDATA_CFG where /*OP = '61' and*/ LOWER(NAME) = 'drewer number' or LOWER(NAME) = 'drawer no' or LOWER(NAME) = 'box number';
--6104: 248
--6103: 260
--'Drawer no'
select * from ACC_PROCDATA_CFG where op = '61';
select * from acc_bomitem_setup where op = '60B';

select * 
from acc_procdata_value pv inner join acc_unithistory uh on pv.unithist_id = uh.rec_id
where pv.dataid in (947, 953, 959, 965, 971, 827) and uh.result = 0;

select count(rec_id), result from acc_unithistory where op = '60B' and ctime between '10.09.2019 06:00' and '11.09.2019 06:00' group by result;--Ford
select count(rec_id), result from acc_unithistory where op = '140' and ctime between '10.09.2019 06:00' and '11.09.2019 06:00' group by result;--Fiat
select count(rec_id), result from acc_unithistory where op = '61' and ctime between '12.09.2019 06:00' and '13.09.2019 06:00' group by result;--MQB