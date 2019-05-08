using System;
using Xunit;

namespace Astron.Expressions.Tests
{
    public static class Static
    {
        public static int Prop { get; set; }
    }

    public class Obj
    {
        public readonly int Field = 5;

        public void Method() { }
    }

    public class SubExpressionCompiler<T> : ExpressionCompiler<T> where T : Delegate
    {
        public int ExpressionsCount => Expressions.Count;
        public int ContentCount => Count();
        public int ParametersCount => Parameters.Count;
        public int VariablesCount => Variables.Count;
    }

    public class ExpressionCompilerTests
    {
        [Fact]
        public void Parameter_T_ShouldRegisterExpression()
        {
            var comp = new SubExpressionCompiler<Action<int>>();
            comp.Parameter<int>("i");

            Assert.Equal(0, comp.ContentCount);
            Assert.Equal(0, comp.VariablesCount);
            Assert.Equal(1, comp.ParametersCount);
            Assert.Equal(1, comp.ExpressionsCount);
        }

        [Fact]
        public void Variable_T_ShouldRegisterExpression()
        {
            var comp = new SubExpressionCompiler<Action>();
            comp.Variable<int>("i");

            Assert.Equal(0, comp.ContentCount);
            Assert.Equal(1, comp.VariablesCount);
            Assert.Equal(0, comp.ParametersCount);
            Assert.Equal(1, comp.ExpressionsCount);
        }

        [Fact]
        public void Constant_ShouldRegisterExpression()
        {
            var comp = new SubExpressionCompiler<Action>();
            comp.Constant(5);

            Assert.Equal(0, comp.ContentCount);
            Assert.Equal(0, comp.VariablesCount);
            Assert.Equal(0, comp.ParametersCount);
            Assert.Equal(1, comp.ExpressionsCount);
        }

        [Fact]
        public void Property_ShouldRegisterExpression()
        {
            var comp = new SubExpressionCompiler<Action>();
            var lengthPi = typeof(string).GetProperty("Length");

            var str = comp.Variable<string>("str");
            var strLength = comp.Property(str, lengthPi);

            Assert.Equal(0, comp.ContentCount);
            Assert.Equal(1, comp.VariablesCount);
            Assert.Equal(0, comp.ParametersCount);
            Assert.Equal(2, comp.ExpressionsCount);
        }

        [Fact]
        public void StaticProperty_ShouldRegisterExpression()
        {
            var comp = new SubExpressionCompiler<Action>();
            var lengthPi = typeof(Static).GetProperty("Prop");

            var strLength = comp.StaticProperty(lengthPi);

            Assert.Equal(0, comp.ContentCount);
            Assert.Equal(0, comp.VariablesCount);
            Assert.Equal(0, comp.ParametersCount);
            Assert.Equal(1, comp.ExpressionsCount);
        }

        [Fact]
        public void Field_ShouldRegisterExpression()
        {
            var comp = new SubExpressionCompiler<Action>();
            var fieldFi = typeof(Obj).GetField("Field");

            var objVar = comp.Variable<Obj>("obj");
            var field = comp.Field(objVar, fieldFi);

            Assert.Equal(0, comp.ContentCount);
            Assert.Equal(1, comp.VariablesCount);
            Assert.Equal(0, comp.ParametersCount);
            Assert.Equal(2, comp.ExpressionsCount);
        }

        [Fact]
        public void StaticField_ShouldRegisterExpression()
        {
            var comp = new SubExpressionCompiler<Action>();
            var emptyFi = typeof(string).GetField("Empty");
            var emptyStr = comp.StaticField(emptyFi);

            Assert.Equal(0, comp.ContentCount);
            Assert.Equal(0, comp.VariablesCount);
            Assert.Equal(0, comp.ParametersCount);
            Assert.Equal(1, comp.ExpressionsCount);
        }

        [Fact]
        public void GetParamIndex_T_ShouldFind()
        {
            var comp = new ExpressionCompiler<Action<int>>();
            var realIndex = comp.Parameter<int>("i");
            var foundIndex = comp.GetParamIndex<int>("i");

            Assert.Equal(realIndex, foundIndex);
        }

        [Fact]
        public void GetPropertyIndex_ShouldFind()
        {
            var comp = new ExpressionCompiler<Action>();
            var lengthPi = typeof(string).GetProperty("Length");

            var str = comp.Variable<string>("str");
            var realIndex = comp.Property(str, lengthPi);
            var foundIndex = comp.GetPropertyIndex(lengthPi);

            Assert.Equal(realIndex, foundIndex);
        }

        [Fact]
        public void GetFieldIndex_ShouldFind()
        {
            var comp = new ExpressionCompiler<Action>();
            var fieldFi = typeof(Obj).GetField("Field");

            var objVar = comp.Variable<Obj>("obj");
            var realIndex = comp.Field(objVar, fieldFi);
            var foundIndex = comp.GetFieldIndex(fieldFi);

            Assert.Equal(realIndex, foundIndex);
        }

        [Fact]
        public void Assign_ShouldRegisterExpression()
        {
            var comp = new SubExpressionCompiler<Func<Obj>>();
            var objVar = comp.Variable<Obj>("obj");
            var newObj = comp.New<Obj>();
            var assign = comp.Assign(objVar, newObj);

            Assert.Equal(0, comp.ContentCount);
            Assert.Equal(1, comp.VariablesCount);
            Assert.Equal(0, comp.ParametersCount);
            Assert.Equal(3, comp.ExpressionsCount);
        }

        [Fact]
        public void Convert_T_ShouldRegisterExpression()
        {
            var comp = new SubExpressionCompiler<Func<Obj>>();
            var objVar = comp.Variable<Obj>("obj");
            var newObj = comp.Convert<object>(objVar);

            Assert.Equal(0, comp.ContentCount);
            Assert.Equal(1, comp.VariablesCount);
            Assert.Equal(0, comp.ParametersCount);
            Assert.Equal(2, comp.ExpressionsCount);
        }

        [Fact]
        public void CastOrUnbox_ShouldRegisterExpression()
        {
            var comp = new SubExpressionCompiler<Func<Obj>>();
            var objVar = comp.Variable<Obj>("obj");
            var newObj = comp.CastOrUnbox(objVar, typeof(object));

            Assert.Equal(0, comp.ContentCount);
            Assert.Equal(1, comp.VariablesCount);
            Assert.Equal(0, comp.ParametersCount);
            Assert.Equal(2, comp.ExpressionsCount);
        }

        [Fact]
        public void New_T_ShouldRegisterExpression()
        {
            var comp = new SubExpressionCompiler<Func<Obj>>();
            var newObj = comp.New<Obj>();

            Assert.Equal(0, comp.ContentCount);
            Assert.Equal(0, comp.VariablesCount);
            Assert.Equal(0, comp.ParametersCount);
            Assert.Equal(1, comp.ExpressionsCount);
        }

        [Fact]
        public void New_Type_ShouldRegisterExpression()
        {
            var comp = new SubExpressionCompiler<Func<Obj>>();
            var newObj = comp.New(typeof(Obj));

            Assert.Equal(0, comp.ContentCount);
            Assert.Equal(0, comp.VariablesCount);
            Assert.Equal(0, comp.ParametersCount);
            Assert.Equal(1, comp.ExpressionsCount);
        }

        [Fact]
        public void Call_ShouldRegisterExpression()
        {
            var comp = new SubExpressionCompiler<Func<Obj>>();
            var methodMi = typeof(Obj).GetMethod("Method");
            var objVar = comp.Variable<Obj>("obj");
            var callMethod = comp.Call(methodMi, objVar);

            Assert.Equal(0, comp.ContentCount);
            Assert.Equal(1, comp.VariablesCount);
            Assert.Equal(0, comp.ParametersCount);
            Assert.Equal(2, comp.ExpressionsCount);
        }

        [Fact]
        public void StaticCall_ShouldRegisterExpression()
        {
            var comp = new SubExpressionCompiler<Func<Obj>>();
            var methodMi = typeof(Math).GetMethod("Acos");
            var arg = comp.Constant((double)5);
            var callMethod = comp.StaticCall(methodMi, arg);

            Assert.Equal(0, comp.ContentCount);
            Assert.Equal(0, comp.VariablesCount);
            Assert.Equal(0, comp.ParametersCount);
            Assert.Equal(2, comp.ExpressionsCount);
        }

        [Fact]
        public void EmitAssign_ShouldRegisterContent()
        {
            var comp = new SubExpressionCompiler<Func<Obj>>();
            var objVar = comp.Variable<Obj>("obj");
            var newObj = comp.New<Obj>();

            comp.EmitAssign(objVar, newObj);
            Assert.Equal(1, comp.ContentCount);
            Assert.Equal(1, comp.VariablesCount);
            Assert.Equal(0, comp.ParametersCount);
            Assert.Equal(2, comp.ExpressionsCount);
        }

        [Fact]
        public void EmitCall_ShouldRegisterContent()
        {
            var comp = new SubExpressionCompiler<Func<Obj>>();
            var methodMi = typeof(Obj).GetMethod("Method");
            var objVar = comp.Variable<Obj>("obj");

            comp.EmitCall(methodMi, objVar);
            Assert.Equal(1, comp.ContentCount);
            Assert.Equal(1, comp.VariablesCount);
            Assert.Equal(0, comp.ParametersCount);
            Assert.Equal(1, comp.ExpressionsCount);
        }

        [Fact]
        public void EmitStaticCall_ShouldRegisterContent()
        {
            var comp = new SubExpressionCompiler<Func<Obj>>();
            var methodMi = typeof(Math).GetMethod("Acos");
            var arg = comp.Constant((double)5);

            comp.EmitStaticCall(methodMi, arg);
            Assert.Equal(1, comp.ContentCount);
            Assert.Equal(0, comp.VariablesCount);
            Assert.Equal(0, comp.ParametersCount);
            Assert.Equal(1, comp.ExpressionsCount);
        }

        [Fact]
        public void Emit_ShouldRegisterContent()
        {
            var comp = new SubExpressionCompiler<Func<Obj>>();
            var methodMi = typeof(Math).GetMethod("Acos");
            var arg = comp.Constant((double)5);
            var callMethod = comp.StaticCall(methodMi, arg);

            comp.Emit(callMethod);
            Assert.Equal(1, comp.ContentCount);
            Assert.Equal(0, comp.VariablesCount);
            Assert.Equal(0, comp.ParametersCount);
            Assert.Equal(2, comp.ExpressionsCount);
        }

        [Fact]
        public void Compile_ShouldCreateDelegate()
        {
            var comp = new ExpressionCompiler<Func<string, int>>();
            var lengthPi = typeof(string).GetProperty("Length");
            var str = comp.Parameter<string>("str");
            var strLength = comp.Property(str, lengthPi);
            comp.Emit(strLength);

            var dlg = comp.Compile();
            Assert.NotNull(dlg);
            Assert.Equal(4, dlg("four"));
        }
    }
}
