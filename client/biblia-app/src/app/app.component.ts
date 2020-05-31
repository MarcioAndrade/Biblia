import { Component } from '@angular/core';
import { Observable } from 'rxjs';

import { BibliaService } from './servicos/biblia.service';
import { CaixaPromessa } from './servicos/caixa-promessa';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  caixaPromessa: CaixaPromessa;
  caixaPromessaRodape: CaixaPromessa;

  title = 'biblia-app';

  constructor(private _service: BibliaService) {
    this._service.getCaixinhaPromessa()
      .subscribe((data: CaixaPromessa) => this.caixaPromessa = data,
        error => console.log(error));
  }

  ObterCaixinhaPromessa(): void {
    this._service.getCaixinhaPromessa()
      .subscribe((data: CaixaPromessa) => this.caixaPromessa = data,
        error => console.log(error));
  }
}
