using _Game.Source.Abstract.DomainGameplay;
using UnityEngine;

namespace _Game.Source.UseCases
{
    public class DrawPointClickValidator: IValidator<DrawValidationContext>
    {
        private readonly RectTransform _drawArea;

        public DrawPointClickValidator(RectTransform drawArea)
        {
            _drawArea = drawArea;
        }

        public bool IsValid(DrawValidationContext context)
        {
            return RectTransformUtility.RectangleContainsScreenPoint(_drawArea, context.MousePosition);
        }
    }

    public struct DrawValidationContext
    {
        public Vector2 MousePosition { get; private set; }
        public DrawValidationContext(Vector2 mousePosition)
        {
            MousePosition = mousePosition;
        }
    }
}