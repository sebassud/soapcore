import http from 'k6/http';
import { sleep, check } from 'k6';

// Otwórz w Visual Studio Code
// Uruchom w terminalu wpisując: .\k6 run program.js

export let options = {
  vus: 100,
  duration: '15s',
  thresholds: {
    http_req_duration: ['p(90)<200'],
  },
  tlsVersion: http.TLS_1_1,
};

const person = {FirstName: "Sebastian", LastName: "Sudra"};

function soapReqBody (request) {
    return `
    <soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:tem="http://tempuri.org/">
       <soapenv:Header/>
       <soapenv:Body>
          <tem:Operation>
             <!--Optional:-->
             <tem:request>
                <!--Optional:-->
                <tem:FirstName>${request.FirstName}</tem:FirstName>
                <!--Optional:-->
                <tem:LastName>${request.LastName}</tem:LastName>
             </tem:request>
          </tem:Operation>
       </soapenv:Body>
    </soapenv:Envelope>`;
}  

export default function () {
    let res = http.post(
        'https://localhost:44329/ErruService.svc',
        soapReqBody(person),
        { headers: { 'Content-Type': 'text/xml' } },
      );

    check(res, {
      'is status 200': (r) => r.status === 200,
      'Result is correct': (r) => r.body.indexOf('Sebastian Sudra') !== -1,
    });
  sleep(1);
}