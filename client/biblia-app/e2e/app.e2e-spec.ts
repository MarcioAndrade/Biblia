import { BibliaAppPage } from './app.po';

describe('biblia-app App', function() {
  let page: BibliaAppPage;

  beforeEach(() => {
    page = new BibliaAppPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
