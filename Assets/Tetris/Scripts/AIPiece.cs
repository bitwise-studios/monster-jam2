public class AIPiece {
	public int row, column;
	public int[,] cells;
	public AIPiece(int row, int column, int[,] cells) {
		this.row = row;
		this.column = column;
		this.cells = cells;
	}
	public AIPiece Clone() {
		return new AIPiece(row, column, (int[,])cells.Clone());
	}
}