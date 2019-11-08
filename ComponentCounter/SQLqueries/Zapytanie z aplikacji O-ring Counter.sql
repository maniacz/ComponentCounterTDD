select h1.szuflada, count(pv.VALUE) iloœæ, h1.itempartno 
from (select itempartno, 
decode(itempartno, 'A0038109', '1', 'A0038110', '2', 'A0035512', '3', 'A0038271', '4', 'A0032886', '5', 'A0038111', '6', 'A0032887', '7', 'A0032888', '8') szuflada 
from  
  (select op, itempartno, decode(itempartno, 'A0038109', '1', 'A0038110', '2', 'A0035512', '3', 'A0038271', '4', 'A0032886', '5', 'A0038111', '6', 'A0032887', '7', 'A0032888', '8') szuflada 
  from acc_bomitem_setup 
  where op = :opDrawer1) 
where op = :opDrawer2 and szuflada is not null 
group by szuflada, itempartno) h1  
  left join 
        ACC_PROCDATA_VALUE pv 
    on h1.szuflada = pv.value 
  and pv.DATAID in ( 193 ) 
    and pv.CTIME between :timeFrom and :timeTo 
group by h1.szuflada, h1.itempartno
order by h1.szuflada;

select REC_ID from ACC_PROCDATA_CFG where OP = :op and LOWER(NAME) = 'drewer number' or LOWER(NAME) = 'drawer no' or LOWER(NAME) = 'box number'