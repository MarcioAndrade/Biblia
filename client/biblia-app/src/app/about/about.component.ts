import { Component, OnInit } from '@angular/core';

import { Versao } from '../versoes/versao';
import { BibliaService } from '../servicos/biblia.service';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html'
})
export class AboutComponent implements OnInit {

  versoes: Versao[];

  constructor(private _service: BibliaService) {
    this._service.getVersoes()
      .subscribe((data: Versao[]) => this.versoes = data,
        error => console.log(error));
  }
  ngOnInit() {
  }

}
