import { Component, OnInit } from '@angular/core';

import { BibliaService } from './../servicos/biblia.service';
import { CaixaPromessa } from './../servicos/caixa-promessa';

@Component({
  selector: 'app-caixa-promessas',
  templateUrl: './caixa-promessas.component.html'
})
export class CaixaPromessasComponent implements OnInit {

  caixaPromessa: CaixaPromessa;
  caixaPromessaRodape: CaixaPromessa;

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

  ngOnInit() {
  }

}
