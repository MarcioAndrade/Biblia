import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { Http, Response } from '@angular/http';

import { AppComponent } from './app.component';
import { CaixinhaPromessaComponent } from './caixinhaPromessa/caixinha-promessa/caixinha-promessa.component';

@NgModule({
  declarations: [
    AppComponent,
    CaixinhaPromessaComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
