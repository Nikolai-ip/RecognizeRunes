using _Game.Source.Domain;
using UnityEngine;

namespace _Game.Source.Application
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