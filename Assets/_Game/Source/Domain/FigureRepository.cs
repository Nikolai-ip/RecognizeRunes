using System.Collections;
using System.Collections.Generic;

namespace _Game.Source.Domain
{
    public class FigureRepository: IRepository<Figure>
    {
        private readonly List<Figure> _figures;

        public FigureRepository(List<Figure> figures)
        {
            _figures = figures;
        }

        public IEnumerator<Figure> GetEnumerator()
        {
            return _figures.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(Figure pin)
        {
            _figures.Add(pin);
        }

        public void Remove(Figure item)
        {
            _figures.Remove(item);
        }
    }
}