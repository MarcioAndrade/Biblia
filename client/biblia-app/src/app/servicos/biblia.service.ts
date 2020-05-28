import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

import { Versiculo } from './versiculo';

@Injectable({
  providedIn: 'root'
})
export class BibliaService {

  // url = 'https://localhost:44340/v1/'; //debug
  url = 'http://localhost/v1/'; // IIS local

  constructor(private http: HttpClient) { }

  getCaixinhaPromessa(): Observable<Versiculo> {
    return this.http.get<Versiculo>(this.url + 'CaixinhaPromessas');
  }
}
