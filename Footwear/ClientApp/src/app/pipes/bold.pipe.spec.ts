import { BoldPipe } from "./bold.pipe";

describe('BoldPipe', () => {
    const pipe = new BoldPipe();


    it('transforms "abc" to "<strong>abc</strong>"', () => {
      expect(pipe.transform('abc')).toBe('<strong>abc</strong>');
    });
});
