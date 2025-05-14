import '../../styles/general/main-container-style.scss';

export default function MainContainer({ children }: { children: React.ReactNode }) {
    return (
        <div className="main-container">
            <div className="main-content">{children}</div>
            <footer className="main-footer">
                <p>© 2025 Taskly. All rights reserved.</p>
            </footer>
        </div>
    );
}