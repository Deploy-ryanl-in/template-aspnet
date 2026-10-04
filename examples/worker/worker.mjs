import net from 'node:net';
setInterval(()=>{const s=net.connect(6379,process.env.REDIS_HOST||'redis',()=>s.write('*2\r\n$4\r\nINCR\r\n$14\r\nworker-counter\r\n'));s.on('data',b=>{console.log('worker counter',b.toString().trim());s.end();});s.on('error',()=>console.error('Redis unavailable'));},5000);
