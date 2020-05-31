import { Component, OnInit } from '@angular/core';

import { Versao } from './versao';
import { BibliaService } from '../servicos/biblia.service';

@Component({
  selector: 'app-versoes',
  templateUrl: './versoes.component.html'
})
export class VersoesComponent implements OnInit {

  versoes: Versao[];

  constructor(private _service: BibliaService) {
    this._service.getVersoes()
      .subscribe((data: Versao[]) => this.versoes = data,
        error => console.log(error));
  }

  ngOnInit() {
  }

}
