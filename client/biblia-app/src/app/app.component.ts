import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { CaixinhaPromessaServiceService } from './service/caixinha-promessa-service.service';
import { Versiculo } from './service/versiculo';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  versiculo: Versiculo;

  title = 'app works!';

  constructor(private _service: CaixinhaPromessaServiceService) {

  }

  ObterVersiculo(): void {
    this._service.getCaixinhaPromessa()
      .subscribe((data: Versiculo) => this.versiculo = data,
        error => console.log(error));
  }
}
