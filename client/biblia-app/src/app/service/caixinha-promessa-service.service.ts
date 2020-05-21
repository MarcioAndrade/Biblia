import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';  
import { Http, Response } from '@angular/http';

import { Versiculo } from './versiculo';

@Injectable()
export class CaixinhaPromessaServiceService {

  url = 'http://mminfotech.com.br/biblia/v1/CaixinhaPromessas';

  constructor(private http: Http) { }

  getCaixinhaPromessa(): Observable<Versiculo> {  
    return this.http.get(this.url)    
    .map((response: Response) => <Versiculo>response.json())
    .do(data => console.log('All: ' + JSON.stringify(data)))
    .catch(this.handleError);;  
  }

  private handleError(error: Response) {
      console.error(error);
      return Observable.throw(error.json().error || 'Server error');
  }
}
//https://imasters.com.br/apis-microsservicos/consumindo-json-via-http-no-angular-js-4