import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { BibliaService } from './servicos/biblia.service';
import { HomeComponent } from './home/home.component';
import { HeaderComponent } from './header/header.component';
import { AboutComponent } from './about/about.component';
import { CaixaPromessasComponent } from './caixa-promessas/caixa-promessas.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    HeaderComponent,
    AboutComponent,
    CaixaPromessasComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule
  ],
  providers: [BibliaService],
  bootstrap: [AppComponent]
})
export class AppModule { }
