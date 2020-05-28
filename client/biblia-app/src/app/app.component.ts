import { Component } from '@angular/core';
import { Observable } from 'rxjs';

import { BibliaService } from './servicos/biblia.service';
import { Versiculo } from './servicos/versiculo';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  versiculo: Versiculo;
  versiculoRodape: Versiculo;

  title = 'biblia-app';

  constructor(private _service: BibliaService) {

  }

  ObterVersiculo(): void {
    this._service.getCaixinhaPromessa()
      .subscribe((data: Versiculo) => this.versiculo = data,
        error => console.log(error));
  }
}
