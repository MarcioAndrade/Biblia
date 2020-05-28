import { Livro } from './livro';

export class Versiculo {
  id: number;
  versaoId: number;
  livro: Livro;
  capitulo: number;
  numero: number;
  texto: string;
}
