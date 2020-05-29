import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

import { CaixaPromessa } from './caixa-promessa';

@Injectable({
  providedIn: 'root'
})
export class BibliaService {

  //url = 'https://localhost:44340/v1/'; //debug
  // url = 'http://localhost/v1/'; // IIS local
  url = 'http://mminfotech.com.br/biblia/v1/';

  constructor(private http: HttpClient) { }

  getCaixinhaPromessa(): Observable<CaixaPromessa> {
    return this.http.get<CaixaPromessa>(this.url + 'CaixinhaPromessas');
  }
}
